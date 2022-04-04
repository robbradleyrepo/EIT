using FuseIT.Sitecore.Personalization.Facets;
using LionTrust.Feature.EXM.Services.Interfaces;
using LionTrust.Foundation.Contact.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.EmailCampaign.Cm;
using Sitecore.EmailCampaign.Cm.Pipelines.HandleBounce;
using Sitecore.EmailCampaign.Cm.Pipelines.HandleDispatchFailed;
using Sitecore.EmailCampaign.XConnect.Web;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Modules.EmailCampaign;
using Sitecore.Modules.EmailCampaign.Core.Contacts;
using Sitecore.Modules.EmailCampaign.Core.Pipelines.HandleMessageEventBase;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LionTrust.Feature.EXM.Pipelines
{
    public class UpdateUndeliveredCount
    {
        private readonly XConnectRetry _xConnectRetry;
        private readonly IContactService _contactService;
        private readonly ISubscriptionManager _subscriptionManager;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public double Delay { get; set; }

        public int RetryCount { get; set; }

        public UpdateUndeliveredCount(
          ILogger logger,
          XConnectRetry xConnectRetry,
          IContactService contactService)
            : this(logger, xConnectRetry, contactService, ServiceProviderServiceExtensions.GetService<ISubscriptionManager>(ServiceLocator.ServiceProvider), ServiceProviderServiceExtensions.GetService<IEmailService>(ServiceLocator.ServiceProvider))
        {
            Assert.ArgumentNotNull((object)logger, nameof(logger));
            Assert.ArgumentNotNull((object)xConnectRetry, nameof(xConnectRetry));
            Assert.ArgumentNotNull((object)contactService, nameof(contactService));
        }

        public UpdateUndeliveredCount(
          ILogger logger,
          XConnectRetry xConnectRetry,
          IContactService contactService,
          ISubscriptionManager subscriptionManager,
          IEmailService emailService)
        {
            Assert.ArgumentNotNull((object)logger, nameof(logger));
            Assert.ArgumentNotNull((object)xConnectRetry, nameof(xConnectRetry));
            Assert.ArgumentNotNull((object)contactService, nameof(contactService));
            Assert.ArgumentNotNull((object)subscriptionManager, nameof(subscriptionManager));
            Assert.ArgumentNotNull((object)contactService, nameof(emailService));
            _logger = logger;
            _xConnectRetry = xConnectRetry;
            _contactService = contactService;
            _subscriptionManager = subscriptionManager;
            _emailService = emailService;
        }

        public string FacetName { get; set; }

        public void Process(HandleMessageEventPipelineArgs args)
        {
            Assert.ArgumentNotNull((object)args, nameof(args));
            if (args is HandleDispatchFailedPipelineArgs failedPipelineArgs && !failedPipelineArgs.UpdateUndeliveredCount)
                return;
            Contact contactWithRetry = _contactService.GetContactWithRetry(args.EventData.ContactIdentifier, Delay, RetryCount, "Emails");
            if (contactWithRetry == null)
            {
                _logger.LogWarn("Failed to updated the undelivered emails count as the contact with identifier: " + args.EventData.ContactIdentifier.ToLogFile() + " cannot be found.");
            }
            else
            {
                UpdateBounceCount(args, contactWithRetry);
            }
        }

        private bool UpdateBounceCount(HandleMessageEventPipelineArgs args, Contact contact)
        {
            Assert.ArgumentNotNull((object)args, nameof(args));
            Assert.ArgumentNotNull((object)contact, nameof(contact));
            try
            {
                EmailAddressList emailAddresses = CollectionModel.Emails(contact);
                EmailAddress preferredEmail = emailAddresses?.PreferredEmail;
                if (preferredEmail != null && preferredEmail.SmtpAddress != null)
                {
                    if (args is HandleBounceArgs handleBounceArgs4 && handleBounceArgs4.IsPermanentFailure)
                    {
                        ManagerRoot managerRoot = args.MessageItem.ManagerRoot;
                        if (managerRoot != null)
                            preferredEmail.BounceCount = managerRoot.Settings.MaxUndelivered;
                    }
                    else
                    {
                        ++preferredEmail.BounceCount;
                    }

                    _xConnectRetry.RequestWithRetry((Action<IXdbContext>)(client =>
                    {
                        CollectionModel.SetEmails(client, (IEntityReference<Contact>)contact, emailAddresses);
                        XConnectSynchronousExtensions.Submit(client);
                    }), new Func<Exception, IXdbContext, bool>(IsTransient), Delay, RetryCount);

                    if (args is HandleBounceArgs handleBounceArgs5 && handleBounceArgs5?.MessageItem?.ManagerRoot != null)
                    {
                        var managerRoot = handleBounceArgs5?.MessageItem?.ManagerRoot;
                        if (preferredEmail.BounceCount >= managerRoot.Settings.MaxUndelivered)
                        {
                            using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                            {
                                var sfEntityUtility = new SFEntityUtility();
                                var sfEntity = sfEntityUtility.GetEntityByEmail(preferredEmail.SmtpAddress);
                                var identifier = sfEntityUtility.GetIdentifier(sfEntity);

                                IdentifiedContactReference reference = new IdentifiedContactReference(Constants.Identifier.S4S, identifier);
                                var expandOptions = new ContactExpandOptions(EmailAddressList.DefaultFacetKey, S4SInfo.DefaultFacetKey);
                                var xdbContact = client.Get<Contact>(reference, expandOptions);

                                var isExcluded = _subscriptionManager.AddToGlobalOptOutList(xdbContact.Identifiers.First(), managerRoot);

                                //update salesforce contact
                                sfEntityUtility.SaveHardBounced(sfEntity);
                            }
                        }
                    }
                }
                return true;
            }
            catch (XdbOperationException ex)
            {
                _logger.LogError(string.Format("Email bounce count not reset. Details: {0}", (object)args), (Exception)ex);
                return false;
            }
        }

        protected bool IsTransient(Exception ex, IXdbContext client)
        {
            IEnumerable<IXdbOperation> ixdbOperations;
            if (client == null)
            {
                ixdbOperations = (IEnumerable<IXdbOperation>)null;
            }
            else
            {
                ReadOnlyCollection<IXdbOperation> lastBatch = client.LastBatch;
                ixdbOperations = lastBatch != null ? ((IEnumerable<IXdbOperation>)lastBatch).Where<IXdbOperation>((Func<IXdbOperation, bool>)(x => x.Status == XdbOperationStatus.Failed)) : (IEnumerable<IXdbOperation>)null;
            }
            IEnumerable<IXdbOperation> source = ixdbOperations;
            if (ex is XdbUnavailableException || source != null && source.Any<IXdbOperation>())
            {
                _logger.LogWarn(FormattableString.Invariant(FormattableStringFactory.Create("[{0}] Transient error. Retrying. (Message: {1})", (object)"SaveInteraction", (object)ex.Message)), ex);
                return true;
            }
            _logger.LogError(FormattableString.Invariant(FormattableStringFactory.Create("[{0}] Not a transient error. Throwing: (Message: {1})", (object)"SaveInteraction", (object)ex.Message)), ex);
            return false;
        }
    }
}