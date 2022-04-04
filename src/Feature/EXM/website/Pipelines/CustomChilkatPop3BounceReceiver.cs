using Chilkat;
using FuseIT.Sitecore.Personalization.Facets;
using LionTrust.Feature.EXM.Services.Interfaces;
using LionTrust.Foundation.Contact.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.EDS.Core.Diagnostics;
using Sitecore.EDS.Core.Net.Pop3;
using Sitecore.EDS.Core.Reporting;
using Sitecore.EDS.Providers.CustomSmtp.Reporting;
using Sitecore.EmailCampaign.Cm;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Modules.EmailCampaign.Services;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LionTrust.Feature.EXM.Pipelines
{
    public class CustomChilkatPop3BounceReceiver : IPop3BounceReceiver
    {
        private readonly string _mailServer;
        private readonly IManagerRootService _managerRootService;
        private readonly IEmailService _emailService;
        private readonly ISubscriptionManager _subscriptionManager;
        private readonly Pop3Settings _pop3Settings;
        private readonly IBounceInspector _inspector;
        private readonly IEnvironmentId _environmentId;
        private readonly ILogger _logger;

        public CustomChilkatPop3BounceReceiver(
          Pop3Settings settings,
          IBounceInspector inspector,
          IEnvironmentId environmentId)
          : this(settings, inspector, environmentId, Sitecore.EDS.Core.Diagnostics.LoggerFactory.Instance)
        {
            Assert.ArgumentNotNull((object)settings, nameof(settings));
            Assert.ArgumentNotNull((object)inspector, nameof(inspector));
            Assert.ArgumentNotNull((object)environmentId, nameof(environmentId));
        }

        public CustomChilkatPop3BounceReceiver(
          Pop3Settings settings,
          IBounceInspector inspector,
          IEnvironmentId environmentId,
          ILoggerFactory loggerFactory)
          : this(settings, inspector, environmentId, loggerFactory.Logger)
        {
            Assert.ArgumentNotNull((object)settings, nameof(settings));
            Assert.ArgumentNotNull((object)inspector, nameof(inspector));
            Assert.ArgumentNotNull((object)environmentId, nameof(environmentId));
            Assert.ArgumentNotNull((object)loggerFactory, nameof(loggerFactory));
        }

        public CustomChilkatPop3BounceReceiver(
          Pop3Settings settings,
          IBounceInspector inspector,
          IEnvironmentId environmentId,
          ILogger logger)
            : this(ServiceProviderServiceExtensions.GetService<IEmailService>(ServiceLocator.ServiceProvider), ServiceProviderServiceExtensions.GetService<IManagerRootService>(ServiceLocator.ServiceProvider), ServiceProviderServiceExtensions.GetService<ISubscriptionManager>(ServiceLocator.ServiceProvider), settings, inspector, environmentId, logger)
        {
            Assert.ArgumentNotNull((object)settings, nameof(settings));
            Assert.ArgumentNotNull((object)inspector, nameof(inspector));
            Assert.ArgumentNotNull((object)environmentId, nameof(environmentId));
            Assert.ArgumentNotNull((object)logger, nameof(logger));
        }

        public CustomChilkatPop3BounceReceiver(
          IEmailService emailService,
          IManagerRootService managerRootService,
          ISubscriptionManager subscriptionManager,
          Pop3Settings settings,
          IBounceInspector inspector,
          IEnvironmentId environmentId,
          ILogger logger)
        {
            Assert.ArgumentNotNull((object)emailService, nameof(emailService));
            Assert.ArgumentNotNull((object)managerRootService, nameof(managerRootService));
            Assert.ArgumentNotNull((object)subscriptionManager, nameof(subscriptionManager));
            Assert.ArgumentNotNull((object)settings, nameof(settings));
            Assert.ArgumentNotNull((object)inspector, nameof(inspector));
            Assert.ArgumentNotNull((object)environmentId, nameof(environmentId));
            Assert.ArgumentNotNull((object)logger, nameof(logger));
            _mailServer = Sitecore.Configuration.Settings.GetSetting(Constants.Settings.MailServer);
            _emailService = emailService;
            _managerRootService = managerRootService;
            _subscriptionManager = subscriptionManager;
            _pop3Settings = settings;
            _inspector = inspector;
            _environmentId = environmentId;
            _logger = logger;
        }

        public async System.Threading.Tasks.Task ProcessMessages(Func<ICollection<Sitecore.EDS.Core.Reporting.Bounce>, System.Threading.Tasks.Task> handleBounces)
        {
            try
            {
                //sendGrid
                if (_mailServer.Contains("sendgrid"))
                {
                    await ProcessSoftBounces();
                    await ProcessHardBounces();
                }
                //stmp server
                else
                {
                    using (IPop3Client pop3Client = this.CreatePop3Client())
                    {
                        if (pop3Client.GetMailboxCount() > 0)
                        {
                            IEnumerable<IPop3Мail> mails = pop3Client.GetMails(false);
                            List<Sitecore.EDS.Core.Reporting.Bounce> bouncedMessages = new List<Sitecore.EDS.Core.Reporting.Bounce>();
                            foreach (IPop3Мail pop3Мail in mails)
                            {
                                BounceStatus bounceType = InspectEmail(pop3Мail, pop3Client);
                                if (bounceType != BounceStatus.NotBounce)
                                {
                                    IPop3Мail mail = pop3Client.GetMail(pop3Мail.Uidl);
                                    if (mail != null && _environmentId.IsMatching(mail.GetHeader("X-Sitecore-EnvironmentId")))
                                        bouncedMessages.Add(MapToBounce(mail, bounceType));
                                }
                            }
                            if (bouncedMessages.Count > 0)
                            {
                                _logger.LogInfo(string.Format("ChilkatPop3BounceReceiver processed {0} bounces", (object)bouncedMessages.Count));
                                await handleBounces((ICollection<Sitecore.EDS.Core.Reporting.Bounce>)bouncedMessages);
                                pop3Client.DeleteMultipleMails(bouncedMessages.Select<Sitecore.EDS.Core.Reporting.Bounce, string>((Func<Sitecore.EDS.Core.Reporting.Bounce, string>)(message => message.Id)));
                            }
                            bouncedMessages = (List<Sitecore.EDS.Core.Reporting.Bounce>)null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }

        protected virtual IPop3Client CreatePop3Client() => (IPop3Client)new ChilkatPop3Client(_pop3Settings);

        private Sitecore.EDS.Core.Reporting.Bounce MapToBounce(
          IPop3Мail pop3Мail,
          BounceStatus bounceType)
        {
            Sitecore.EDS.Core.Reporting.Bounce bounce = new Sitecore.EDS.Core.Reporting.Bounce();
            bounce.Id = pop3Мail.Uidl;
            bounce.MessageId = pop3Мail.GetHeader("X-BatchID");
            bounce.CampaignId = pop3Мail.GetHeader("X-Sitecore-Campaign");
            bounce.ContactId = pop3Мail.GetHeader("X-MessageID");
            bounce.InstanceId = pop3Мail.GetHeader("X-BatchID");
            bounce.BounceType = bounceType;
            return bounce;
        }

        public virtual BounceStatus InspectEmail(IPop3Мail pop3Мail, IPop3Client pop3Client)
        {
            BounceStatus bounceStatus = _inspector.InspectMime(pop3Мail.GetMime);
            if (bounceStatus != BounceStatus.NotBounce || !(pop3Client is MailMan mailMan))
                return bounceStatus;
            Email email = mailMan.FetchEmail(pop3Мail.Uidl);
            if (email == null)
                return bounceStatus;
            ChilkatBounceInspector inspector = (ChilkatBounceInspector)_inspector;
            if (inspector.ExamineEmail(email))
            {
                bounceStatus = inspector.MapChilkatBounceToBounce(inspector.BounceType);
            }
            else
            {
                int result;
                if (!email.IsMultipartReport() || !int.TryParse(email.GetDeliveryStatusInfo("Status").Replace(".", string.Empty), out result))
                    return bounceStatus;
                if (result >= 200 && result < 300)
                    bounceStatus = BounceStatus.NotBounce;
                else if (result >= 400 && result < 500)
                    bounceStatus = BounceStatus.SoftBounce;
                else if (result >= 500 && result < 600)
                    bounceStatus = BounceStatus.HardBounce;
            }
            return bounceStatus;
        }

        private async System.Threading.Tasks.Task ProcessSoftBounces()
        {
            var sfEntityUtility = new SFEntityUtility();
            var bounces = await _emailService.GetSoftBounces();
            var excludeContacts = new List<KeyValuePair<Contact, FuseIT.Sitecore.SalesforceConnector.Entities.EntityBase>>();

            var managerRoot = _managerRootService.GetManagerRoots()?.FirstOrDefault();

            if (bounces.Any())
            {
                using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                {
                    try
                    {
                        foreach (var bouncedEmail in bounces)
                        {
                            var email = bouncedEmail.Email;
                            var sfEntity = sfEntityUtility.GetEntityByEmail(email);
                            var identifier = sfEntityUtility.GetIdentifier(sfEntity);

                            IdentifiedContactReference reference = new IdentifiedContactReference(Constants.Identifier.S4S, identifier);
                            var expandOptions = new ContactExpandOptions(EmailAddressList.DefaultFacetKey, S4SInfo.DefaultFacetKey);
                            var xdbContact = client.Get<Contact>(reference, expandOptions);

                            var emails = xdbContact.Emails();

                            if (emails.PreferredEmail.BounceCount < managerRoot.Settings.MaxUndelivered)
                            {
                                emails.PreferredEmail.BounceCount = emails.PreferredEmail.BounceCount + 1;
                            }
                            else
                            {
                                emails.PreferredEmail.BounceCount = managerRoot.Settings.MaxUndelivered;
                            }

                            if (emails.PreferredEmail.BounceCount >= managerRoot.Settings.MaxUndelivered)
                            {
                                excludeContacts.Add(new KeyValuePair<Contact, FuseIT.Sitecore.SalesforceConnector.Entities.EntityBase>(xdbContact, sfEntity));
                            }

                            client.SetFacet(xdbContact, EmailAddressList.DefaultFacetKey, emails);
                            excludeContacts.Add(new KeyValuePair<Contact, FuseIT.Sitecore.SalesforceConnector.Entities.EntityBase>(xdbContact, sfEntity));
                        }

                        client.Submit();

                        foreach (var exclude in excludeContacts)
                        {
                            var isExcluded = managerRoot.GlobalSubscription.AddToDefaultExcludeCollection(exclude.Key);
                            var emails = exclude.Key.Emails();
                            _subscriptionManager.AddToGlobalOptOutList(exclude.Key.Identifiers.First(), managerRoot);

                            //update salesforce contact
                            sfEntityUtility.SaveHardBounced(exclude.Value);

                            var result = await _emailService.DeleteSoftBounce(emails.PreferredEmail.SmtpAddress);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex);
                    }
                }
            }
        }

        private async System.Threading.Tasks.Task ProcessHardBounces()
        {
            var sfEntityUtility = new SFEntityUtility();
            var bounces = await _emailService.GetHardBounces();
            var excludeContacts = new List<KeyValuePair<Contact, FuseIT.Sitecore.SalesforceConnector.Entities.EntityBase>>();

            var managerRoot = _managerRootService.GetManagerRoots()?.FirstOrDefault();

            if (bounces.Any())
            {
                using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                {
                    try
                    {
                        foreach (var bouncedEmail in bounces)
                        {
                            var email = bouncedEmail.Email;
                            var sfEntity = sfEntityUtility.GetEntityByEmail(email);
                            var identifier = sfEntityUtility.GetIdentifier(sfEntity);

                            IdentifiedContactReference reference = new IdentifiedContactReference(Constants.Identifier.S4S, identifier);
                            var expandOptions = new ContactExpandOptions(EmailAddressList.DefaultFacetKey, S4SInfo.DefaultFacetKey);
                            var xdbContact = client.Get<Contact>(reference, expandOptions);

                            var emails = xdbContact.Emails();
                            emails.PreferredEmail.BounceCount = managerRoot.Settings.MaxUndelivered;

                            client.SetFacet(xdbContact, EmailAddressList.DefaultFacetKey, emails);
                            excludeContacts.Add(new KeyValuePair<Contact, FuseIT.Sitecore.SalesforceConnector.Entities.EntityBase>(xdbContact, sfEntity));
                        }

                        client.Submit();

                        foreach (var exclude in excludeContacts)
                        {
                            var emails = exclude.Key.Emails();
                            var isExcluded = _subscriptionManager.AddToGlobalOptOutList(exclude.Key.Identifiers.First(), managerRoot);

                            //update salesforce contact
                            sfEntityUtility.SaveHardBounced(exclude.Value);

                            var result = await _emailService.DeleteHardBounce(emails.PreferredEmail.SmtpAddress);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex);
                    }
                }
            }
        }
    }
}