using FuseIT.Sitecore.Personalization.Facets;
using Glass.Mapper.Sc;
using LionTrust.Feature.EXM.Helpers.Interfaces;
using LionTrust.Feature.EXM.Models;
using LionTrust.Foundation.Contact.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Abstractions;
using Sitecore.DependencyInjection;
using Sitecore.EDS.Core.Dispatch;
using Sitecore.EmailCampaign.Cm.Pipelines.SendEmail;
using Sitecore.Modules.EmailCampaign.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LionTrust.Feature.EXM.Helpers.Implementations
{
    public class FillMailHelper : IFillEmailHelper
    {
        private readonly ISitecoreService _sitecoreService;
        private readonly ISFEntityUtility _sfEntityUtility;
        private readonly BaseSettings _settings;

        private readonly char[] separators = new char[] { ',', ';' };
        private const string SENDER = "Sender";
        private const string SALESFORCECAMPAIGN = "SalesforceCampaignId";

        public FillMailHelper(BaseSettings settings)
            : this(settings, new SitecoreService("web"), ServiceLocator.ServiceProvider.GetService<ISFEntityUtility>())
        {
        }

        private FillMailHelper(BaseSettings settings, ISitecoreService sitecoreService, ISFEntityUtility sfEntityUtility)
        {
            _sfEntityUtility = sfEntityUtility;
            _settings = settings;
            _sitecoreService = sitecoreService;
        }

        public void FillEmail(MailMessageItem mailMessageItem, SendMessageArgs sendMessageArgs, EmailMessage emailMessage)
        {
            var s4sInfo = mailMessageItem?.PersonalizationRecipient?.GetFacet<S4SInfo>(S4SInfo.DefaultFacetKey);
            SetFrom(emailMessage, s4sInfo);
            SetSalesforceCampaignId(emailMessage, mailMessageItem.ID);

            emailMessage.Recipients = SetRecipients(emailMessage);
            emailMessage.Recipients = ExcludeUnsubscribedContacts(emailMessage, sendMessageArgs.IsTestSend);
        }

        private void SetFrom(EmailMessage emailMessage, S4SInfo info)
        {
            if (info == null)
            {
                return;
            }

            if (info.Fields.ContainsKey(Foundation.Contact.Constants.SF_Owner_EmailField))
            {
                emailMessage.FromAddress = info.Fields[Foundation.Contact.Constants.SF_Owner_EmailField];
            }
            if (info.Fields.ContainsKey(Foundation.Contact.Constants.SF_Owner_NameField))
            {
                var ownerName = info.Fields[Foundation.Contact.Constants.SF_Owner_NameField];
                emailMessage.FromName = ownerName;

                if (emailMessage.Headers[SENDER] != null)
                {
                    emailMessage.Headers[SENDER] = ownerName;
                }
                else
                {
                    emailMessage.Headers.Add(SENDER, ownerName);
                }
            }
        }

        private void SetSalesforceCampaignId(EmailMessage emailMessage, string messageId)
        {
            if (string.IsNullOrWhiteSpace(messageId))
            {
                return;
            }

            var mailMessage = _sitecoreService.GetItem<Models.IMailMessage>(new Guid(messageId));
            var contactList = mailMessage?.IncludedRecipientLists?.FirstOrDefault();
            var salesforceCampaignId = contactList?.SalesforceCampaignId;

            if (!string.IsNullOrWhiteSpace(salesforceCampaignId))
            {
                emailMessage.Headers.Add(SALESFORCECAMPAIGN, salesforceCampaignId);
            }
        }

        private List<string> SetRecipients(EmailMessage emailMessage)
        {
            //is testing environment
            var testingEnv = _settings.GetBoolSetting(Constants.Settings.MailTestingEnvironment, true);
            if (testingEnv)
            {
                var exmSettings = _sitecoreService.GetItem<IExmSettings>(Constants.ExmSettings.ExmSettings_ItemID);
                var whitelistDomains = exmSettings.WhitelistDomains.Split(separators)?.Select(x => x.Trim());
                var testingRecipients = exmSettings.TestingRecipientList.Split(separators)?.Select(x => x.Trim());

                var recipients = new List<string>();
                foreach (var r in emailMessage.Recipients)
                {
                    var domain = r.Split('@')[1];
                    if (whitelistDomains.Any(x => x == domain))
                    {
                        recipients.Add(r);
                    }
                }

                if (!recipients.Any())
                {
                    recipients.AddRange(testingRecipients);
                }

                return recipients;
            }
            else
            {
                return emailMessage.Recipients;
            }
        }

        private List<string> ExcludeUnsubscribedContacts(EmailMessage emailMessage, bool isTestSend)
        {
            // exclude unsubscribed contacts
            var subscribedRecipients = new List<string>();

            if (isTestSend == false)
            {
                foreach (var recipient in emailMessage.Recipients)
                {
                    var isUnsubscribedOrHardBounced = _sfEntityUtility.IsUnsubscribedOrHardBounced(recipient);
                    if (!isUnsubscribedOrHardBounced)
                    {
                        subscribedRecipients.Add(recipient);
                    }
                }

                return subscribedRecipients;
            }
            else
            {
                return emailMessage.Recipients;
            }
        }
    }
}