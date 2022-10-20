using Chilkat;
using FuseIT.Sitecore.Personalization.Facets;
using LionTrust.Feature.EXM.Services.Implementations;
using LionTrust.Feature.EXM.Services.Interfaces;
using LionTrust.Foundation.Contact.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.EDS.Core.Diagnostics;
using Sitecore.EDS.Core.Net.Pop3;
using Sitecore.EDS.Core.Reporting;
using Sitecore.EDS.Providers.CustomSmtp.Reporting;
using Sitecore.EmailCampaign.Model.Data;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Modules.EmailCampaign.Services;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ContactConstants = LionTrust.Foundation.Contact.Constants;

namespace LionTrust.Feature.EXM.Pipelines
{
    public class CustomChilkatPop3BounceReceiver : IPop3BounceReceiver
    {
        private const string SendGrid = "sendgrid";

        private readonly IManagerRootService _managerRootService;
        private readonly ISFEntityUtility _sfEntityUtility;
        private readonly IEmailService _emailService;
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
        }

        public CustomChilkatPop3BounceReceiver(
          Pop3Settings settings,
          IBounceInspector inspector,
          IEnvironmentId environmentId,
          ILoggerFactory loggerFactory)
          : this(settings, inspector, environmentId, loggerFactory.Logger)
        {
        }

        public CustomChilkatPop3BounceReceiver(
          Pop3Settings settings,
          IBounceInspector inspector,
          IEnvironmentId environmentId,
          ILogger logger)
            : this(new SendGridEmailService(settings.Password),
                  ServiceLocator.ServiceProvider.GetService<IManagerRootService>(),
                  ServiceLocator.ServiceProvider.GetService<ISFEntityUtility>(),
                  settings, 
                  inspector, 
                  environmentId, 
                  logger)
        {
        }

        public CustomChilkatPop3BounceReceiver(
          IEmailService emailService,
          IManagerRootService managerRootService,
          ISFEntityUtility sfEntityUtility,
          Pop3Settings settings,
          IBounceInspector inspector,
          IEnvironmentId environmentId,
          ILogger logger)
        {
            Assert.ArgumentNotNull(emailService, nameof(emailService));
            Assert.ArgumentNotNull(managerRootService, nameof(managerRootService));
            Assert.ArgumentNotNull(sfEntityUtility, nameof(sfEntityUtility));
            Assert.ArgumentNotNull(settings, nameof(settings));
            Assert.ArgumentNotNull(inspector, nameof(inspector));
            Assert.ArgumentNotNull(environmentId, nameof(environmentId));
            Assert.ArgumentNotNull(logger, nameof(logger));

            _emailService = emailService;
            _managerRootService = managerRootService;
            _sfEntityUtility = sfEntityUtility;
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
                if (_pop3Settings.Server.Contains(SendGrid))
                {
                    var bounces = await _emailService.GetBounces();
                    var softBounces = bounces.Where(x => !x.HardBounce).ToList();
                    var hardBounces = bounces.Where(x => x.HardBounce).ToList();
                    await ProcessSoftBounces(softBounces);
                    await ProcessHardBounces(hardBounces);
                }
                //stmp server
                else
                {
                    using (IPop3Client pop3Client = CreatePop3Client())
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
                                    {
                                        bouncedMessages.Add(MapToBounce(mail, bounceType));
                                    }
                                }
                            }
                            if (bouncedMessages.Count > 0)
                            {
                                _logger.LogInfo(string.Format("ChilkatPop3BounceReceiver processed {0} bounces", bouncedMessages.Count));
                                await handleBounces(bouncedMessages);
                                pop3Client.DeleteMultipleMails(bouncedMessages.Select(message => message.Id));
                            }
                            bouncedMessages = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }

        protected virtual IPop3Client CreatePop3Client() => new ChilkatPop3Client(_pop3Settings);

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
            {
                return bounceStatus;
            }

            var email = mailMan.FetchEmail(pop3Мail.Uidl);
            if (email == null)
            {
                return bounceStatus;
            }

            var inspector = (ChilkatBounceInspector)_inspector;
            if (inspector.ExamineEmail(email))
            {
                bounceStatus = inspector.MapChilkatBounceToBounce(inspector.BounceType);
            }
            else
            {
                if (!email.IsMultipartReport() || !int.TryParse(email.GetDeliveryStatusInfo(ColumnConstants.Status).Replace(".", string.Empty), out int result))
                {
                    return bounceStatus;
                }

                if (result >= 200 && result < 300)
                {
                    bounceStatus = BounceStatus.NotBounce;
                }
                else if (result >= 400 && result < 500)
                {
                    bounceStatus = BounceStatus.SoftBounce;
                }
                else if (result >= 500 && result < 600)
                {
                    bounceStatus = BounceStatus.HardBounce;
                }
            }
            return bounceStatus;
        }

        private async System.Threading.Tasks.Task ProcessSoftBounces(List<Models.Bounce> bounces)
        {
            await ExecuteSoftBounces(bounces);

            var blocks = await _emailService.GetBlocks();
            await ExecuteSoftBounces(blocks, true);
        }

        private async System.Threading.Tasks.Task ExecuteSoftBounces(List<Models.Bounce> list, bool isBlockEmail = false)
        {
            var excludeContacts = new List<KeyValuePair<Contact, FuseIT.Sitecore.SalesforceConnector.Entities.EntityBase>>();

            var managerRoot = _managerRootService.GetManagerRoots()?.FirstOrDefault();

            if (list.Any())
            {
                using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                {
                    try
                    {
                        foreach (var bouncedEmail in list)
                        {
                            var email = bouncedEmail.Email;
                            var sfEntity = _sfEntityUtility.GetEntityByEmail(email);
                            var identifier = _sfEntityUtility.GetIdentifier(sfEntity);

                            var reference = new IdentifiedContactReference(ContactConstants.Identifier.S4S, identifier);
                            var expandOptions = new ContactExpandOptions(EmailAddressList.DefaultFacetKey, S4SInfo.DefaultFacetKey);
                            var xdbContact = client.Get(reference, expandOptions);

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
                            var emails = exclude.Key.Emails();

                            //update salesforce contact
                            _sfEntityUtility.SaveHardBounced(exclude.Value);

                            var result = isBlockEmail 
                                ? await _emailService.DeleteBlock(emails.PreferredEmail.SmtpAddress)
                                : await _emailService.DeleteBounce(emails.PreferredEmail.SmtpAddress);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex);
                    }
                }
            }
        }

        private async System.Threading.Tasks.Task ProcessHardBounces(List<Models.Bounce> bounces)
        {
            var excludeContacts = new List<KeyValuePair<Contact, FuseIT.Sitecore.SalesforceConnector.Entities.EntityBase>>();

            var managerRoot = _managerRootService.GetManagerRoots()?.FirstOrDefault();

            if (managerRoot != null && bounces.Any())
            {
                using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                {
                    try
                    {
                        foreach (var bouncedEmail in bounces)
                        {
                            var email = bouncedEmail.Email;
                            var sfEntity = _sfEntityUtility.GetEntityByEmail(email);
                            var identifier = _sfEntityUtility.GetIdentifier(sfEntity);

                            var reference = new IdentifiedContactReference(ContactConstants.Identifier.S4S, identifier);
                            var expandOptions = new ContactExpandOptions(EmailAddressList.DefaultFacetKey, S4SInfo.DefaultFacetKey);
                            var xdbContact = client.Get(reference, expandOptions);

                            var emails = xdbContact.Emails();
                            emails.PreferredEmail.BounceCount = managerRoot.Settings.MaxUndelivered;

                            client.SetFacet(xdbContact, EmailAddressList.DefaultFacetKey, emails);
                            excludeContacts.Add(new KeyValuePair<Contact, FuseIT.Sitecore.SalesforceConnector.Entities.EntityBase>(xdbContact, sfEntity));
                        }

                        client.Submit();

                        foreach (var exclude in excludeContacts)
                        {
                            var emails = exclude.Key.Emails();

                            //update salesforce contact
                            _sfEntityUtility.SaveHardBounced(exclude.Value);

                            var result = await _emailService.DeleteBounce(emails.PreferredEmail.SmtpAddress);
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