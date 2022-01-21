using Glass.Mapper.Sc;
using LionTrust.Feature.EXM.Helpers.Interfaces;
using LionTrust.Feature.EXM.Models;
using LionTrust.Foundation.Contact.Models;
using LionTrust.Foundation.Contact.Services;
using Sitecore.Abstractions;
using Sitecore.EDS.Core.Dispatch;
using Sitecore.EmailCampaign.Cm.Pipelines.SendEmail;
using Sitecore.Modules.EmailCampaign.Messages;
using Sitecore.XConnect;
using System.Collections.Generic;
using System.Linq;

namespace LionTrust.Feature.EXM.Helpers.Implementations
{
    public class FillMailHelper : IFillEmailHelper
    {
        private readonly SitecoreContactUtility _scContactUtility;
        private readonly BaseSettings _settings;

        private char[] separators = new char[]{ ',', ';' };

        public FillMailHelper(BaseSettings settings)
        {
            _scContactUtility = new SitecoreContactUtility();
            _settings = settings;
        }

        public void FillEmail(MailMessageItem mailMessageItem, SendMessageArgs sendMessageArgs, EmailMessage emailMessage)
        {
            var testingEnv = _settings.GetBoolSetting(Constants.Settings.MailTestingEnvironment, true);
            if (testingEnv)
            {
                var publishedDatabase = Sitecore.Data.Database.GetDatabase("web");
                var sitecoreService = new SitecoreService(publishedDatabase);

                var exmSettings = sitecoreService.GetItem<IExmSettings>(Constants.ExmSettings.ExmSettings_ItemID);
                var whitelistDomains = exmSettings.WhitelistDomains.Split(separators)?.Select(x => x.Trim());
                var testingRecipients = exmSettings.TestingRecipientList.Split(separators)?.Select(x => x.Trim());

                var recipients = new List<string>();
                foreach(var r in emailMessage.Recipients)
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

                emailMessage.Recipients = recipients;
            }

            //send quick test
            if (mailMessageItem.ContactIdentifier == null)
            {
                IReadOnlyCollection<ContactIdentifier> identifiers = null;
                switch (sendMessageArgs.EcmMessage)
                {
                    case ABTestMessage testMessage:
                        identifiers = testMessage.PersonalizationRecipient?.Identifiers;
                        break;
                    case TextMail textMail:
                        identifiers = textMail.PersonalizationRecipient?.Identifiers;
                        break;

                }
                    
                if (identifiers != null)
                {
                    foreach (var id in identifiers)
                    {
                        if (id.Source != Sitecore.XConnect.Constants.AliasIdentifierSource) continue;

                        var scContactFacetData = _scContactUtility.GetCurrentSitecoreContactFacetData(id.Identifier);

                        SetFrom(emailMessage, scContactFacetData);
                    }
                }
            }
            else
            {
                var contactId = mailMessageItem.ContactIdentifier.Identifier;
                var scContactFacetData = _scContactUtility.GetCurrentSitecoreContactFacetData(contactId);

                SetFrom(emailMessage, scContactFacetData);
            }
        }

        private void SetFrom(EmailMessage emailMessage, ScContactFacetData scContactFacetData)
        {
            if (!string.IsNullOrWhiteSpace(scContactFacetData?.OwnerEmail))
            {
                emailMessage.FromAddress = scContactFacetData.OwnerEmail;
            }
            if (!string.IsNullOrWhiteSpace(scContactFacetData?.OwnerName))
            {
                emailMessage.FromName = scContactFacetData.OwnerName;

                if (emailMessage.Headers["Sender"] != null)
                {
                    emailMessage.Headers["Sender"] = scContactFacetData.OwnerName;
                }
                else
                {
                    emailMessage.Headers.Add("Sender", scContactFacetData.OwnerName);
                }
            }
        }
    }
}