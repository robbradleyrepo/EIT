using FuseIT.Sitecore.Personalization.Facets;
using Glass.Mapper.Sc;
using LionTrust.Feature.EXM.Helpers.Interfaces;
using LionTrust.Feature.EXM.Models;
using Sitecore.Abstractions;
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
        private readonly BaseSettings _settings;

        private readonly char[] separators = new char[] { ',', ';' };
        private const string SENDER = "Sender";
        private const string SALESFORCECAMPAIGN = "SalesforceCampaignId";

        public FillMailHelper(BaseSettings settings)
            : this(settings, new SitecoreService("web"))
        {
        }

        private FillMailHelper(BaseSettings settings, ISitecoreService sitecoreService)
        {
            _settings = settings;
            _sitecoreService = sitecoreService;
        }

        public void FillEmail(MailMessageItem mailMessageItem, SendMessageArgs sendMessageArgs, EmailMessage emailMessage)
        {
            var s4sInfo = mailMessageItem?.PersonalizationRecipient?.GetFacet<S4SInfo>(S4SInfo.DefaultFacetKey);
            var exmSettings = _sitecoreService.GetItem<IExmSettings>(Constants.ExmSettings.ExmSettings_ItemID);

            SetFrom(emailMessage, s4sInfo, exmSettings);
            SetSalesforceCampaignId(emailMessage, mailMessageItem.ID);

            emailMessage.Recipients = SetRecipients(emailMessage, exmSettings);
        }

        private void SetFrom(EmailMessage emailMessage, S4SInfo info, IExmSettings exmSettings)
        {
            if (info == null)
            {
                return;
            }

            var owner = SFEntityHelper.GetOwner(info);
            
            if (owner == null)
            {
                return;
            }

            var validDomains = exmSettings.ValidDomains.Split(separators)?.Select(x => x.Trim());

            //check if domain is valid
            var domain = owner.Email.Split('@')[1];
            if (!validDomains.Any(x => x == domain))
            {
                return;
            }

            emailMessage.FromAddress = owner.Email;
            emailMessage.FromName = owner.Name;

            if (emailMessage.Headers[SENDER] != null)
            {
                emailMessage.Headers[SENDER] = owner.Name;
            }
            else
            {
                emailMessage.Headers.Add(SENDER, owner.Name);
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

        private List<string> SetRecipients(EmailMessage emailMessage, IExmSettings exmSettings)
        {
            //is testing environment
            var testingEnv = _settings.GetBoolSetting(Constants.Settings.MailTestingEnvironment, true);
            if (testingEnv)
            {
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
    }
}