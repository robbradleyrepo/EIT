namespace LionTrust.Feature.EXM.Pipelines
{
    using Glass.Mapper.Sc;
    using Sitecore.EmailCampaign.Cm.Pipelines.SendEmail;
    using Sitecore.Modules.EmailCampaign.Messages;
    using Sitecore.SecurityModel;
    using System;
    using System.Linq;

    public class UpdateSalesforceCampaign
    {
        private readonly ISitecoreService _sitecoreService;

        public UpdateSalesforceCampaign()
            : this(new SitecoreService("master"))
        {
        }

        private UpdateSalesforceCampaign(ISitecoreService sitecoreService)
        {
            _sitecoreService = sitecoreService;
        }

        public void Process(SendMessageArgs args)
        {
            if (args.EcmMessage == null)
                return;

            if (!args.IsTestSend)
            {
                var messageId = ((MailMessageItem)args.EcmMessage).ID;
                var mailMessage = _sitecoreService.GetItem<Models.IMailMessage>(new Guid(messageId));
                var contactList = mailMessage?.IncludedRecipientLists?.FirstOrDefault();

                if (contactList != null)
                {
                    using (new SecurityDisabler())
                    {
                        contactList.Active = false;
                        _sitecoreService.SaveItem(new SaveOptions(contactList));

                        mailMessage.SalesforceCampaignId = contactList.SalesforceCampaignId;
                        _sitecoreService.SaveItem(new SaveOptions(mailMessage));
                    }
                }
            }
        }
    }
}