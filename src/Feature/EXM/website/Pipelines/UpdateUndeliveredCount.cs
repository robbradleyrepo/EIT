using FuseIT.Sitecore.Personalization.Facets;
using LionTrust.Foundation.Contact.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.EmailCampaign.Cm.Pipelines.HandleBounce;
using Sitecore.EmailCampaign.Cm.Pipelines.HandleDispatchFailed;
using Sitecore.EmailCampaign.XConnect.Web;
using Sitecore.ExM.Framework.Diagnostics;
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
using ContactConstants = LionTrust.Foundation.Contact.Constants;

namespace LionTrust.Feature.EXM.Pipelines
{
    public class UpdateUndeliveredCount
    {
        private readonly XConnectRetry _xConnectRetry;
        private readonly IContactService _contactService;
        private readonly ISFEntityUtility _sfEntityUtility;
        private readonly ILogger _logger;

        public double Delay { get; set; }
        public int RetryCount { get; set; }

        public UpdateUndeliveredCount(
          ILogger logger,
          XConnectRetry xConnectRetry,
          IContactService contactService)
            : this(logger, xConnectRetry, contactService, ServiceLocator.ServiceProvider.GetService<ISFEntityUtility>())
        {
        }

        public UpdateUndeliveredCount(
          ILogger logger,
          XConnectRetry xConnectRetry,
          IContactService contactService,
          ISFEntityUtility sfEntityUtility)
        {
            Assert.ArgumentNotNull(logger, nameof(logger));
            Assert.ArgumentNotNull(xConnectRetry, nameof(xConnectRetry));
            Assert.ArgumentNotNull(contactService, nameof(contactService));
            Assert.ArgumentNotNull(sfEntityUtility, nameof(sfEntityUtility));

            _logger = logger;
            _xConnectRetry = xConnectRetry;
            _contactService = contactService;
            _sfEntityUtility = sfEntityUtility;
        }

        public string FacetName { get; set; }

        public void Process(HandleMessageEventPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            if (args is HandleDispatchFailedPipelineArgs failedPipelineArgs && !failedPipelineArgs.UpdateUndeliveredCount)
            {
                return;
            }

            Contact contactWithRetry = _contactService.GetContactWithRetry(args.EventData.ContactIdentifier, Delay, RetryCount, EmailAddressList.DefaultFacetKey);
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
            Assert.ArgumentNotNull(args, nameof(args));
            Assert.ArgumentNotNull(contact, nameof(contact));

            try
            {
                var emailAddresses = CollectionModel.Emails(contact);
                var preferredEmail = emailAddresses?.PreferredEmail;
                if (preferredEmail?.SmtpAddress != null)
                {
                    if (args is HandleBounceArgs handleBounceArgs4 && handleBounceArgs4.IsPermanentFailure)
                    {
                        var managerRoot = args.MessageItem.ManagerRoot;
                        if (managerRoot != null)
                        {
                            preferredEmail.BounceCount = managerRoot.Settings.MaxUndelivered;
                        }
                    }
                    else
                    {
                        ++preferredEmail.BounceCount;
                    }

                    _xConnectRetry.RequestWithRetry((client =>
                    {
                        CollectionModel.SetEmails(client, contact, emailAddresses);
                        XConnectSynchronousExtensions.Submit(client);
                    }), new Func<Exception, IXdbContext, bool>(IsTransient), Delay, RetryCount);

                    if (args is HandleBounceArgs handleBounceArgs5 && handleBounceArgs5?.MessageItem?.ManagerRoot != null)
                    {
                        var managerRoot = handleBounceArgs5?.MessageItem?.ManagerRoot;
                        if (preferredEmail.BounceCount >= managerRoot.Settings.MaxUndelivered)
                        {
                            using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                            {
                                var sfEntity = _sfEntityUtility.GetEntityByEmail(preferredEmail.SmtpAddress);
                                var identifier = _sfEntityUtility.GetIdentifier(sfEntity);

                                var reference = new IdentifiedContactReference(ContactConstants.Identifier.S4S, identifier);
                                var expandOptions = new ContactExpandOptions(EmailAddressList.DefaultFacetKey, S4SInfo.DefaultFacetKey);
                                var xdbContact = client.Get(reference, expandOptions);

                                //update salesforce contact
                                _sfEntityUtility.SaveHardBounced(sfEntity);
                            }
                        }
                    }
                }
                return true;
            }
            catch (XdbOperationException ex)
            {
                _logger.LogError(string.Format("Email bounce count not reset. Details: {0}", args), ex);
                return false;
            }
        }

        protected bool IsTransient(Exception ex, IXdbContext client)
        {
            IEnumerable<IXdbOperation> ixdbOperations;
            if (client == null)
            {
                ixdbOperations = null;
            }
            else
            {
                ReadOnlyCollection<IXdbOperation> lastBatch = client.LastBatch;
                ixdbOperations = lastBatch?.Where(x => x.Status == XdbOperationStatus.Failed);
            }

            var source = ixdbOperations;
            if (ex is XdbUnavailableException || source != null && source.Any())
            {
                _logger.LogWarn(FormattableString.Invariant(FormattableStringFactory.Create("[{0}] Transient error. Retrying. (Message: {1})", "SaveInteraction", ex.Message)), ex);
                return true;
            }

            _logger.LogError(FormattableString.Invariant(FormattableStringFactory.Create("[{0}] Not a transient error. Throwing: (Message: {1})", "SaveInteraction", ex.Message)), ex);
            return false;
        }
    }
}