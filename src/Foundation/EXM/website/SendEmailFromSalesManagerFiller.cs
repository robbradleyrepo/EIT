namespace LionTrust.Foundation.EXM
{
    using Sitecore.EmailCampaign.Cm.Pipelines.SendEmail;
    using Sitecore.EmailCampaign.Model.Web.Settings;
    using Sitecore.ExM.Framework.Diagnostics;
    using Sitecore.Modules.EmailCampaign.Core;
    using Sitecore.Modules.EmailCampaign.Core.Crypto;
    using Sitecore.Modules.EmailCampaign.Messages;

    public class SendEmailFromSalesManagerFiller
    {
        private readonly IStringCipher _cipher;
        private readonly ILogger _logger;
        private readonly PipelineHelper _pipelineHelper;
        private readonly EcmSettings _settings;

        public SendEmailFromSalesManagerFiller(IStringCipher cipher, ILogger logger, PipelineHelper pipelineHelper, EcmSettings settings)
        {
            _cipher = cipher;
            _logger = logger;
            _pipelineHelper = pipelineHelper;
            _settings = settings;
        }

        public void Process(SendMessageArgs args)
        {
            if (args.EcmMessage == null)
                return;

            if (args.EcmMessage is HtmlMailBase)
            {
                new HtmlMailFillerCustom((HtmlMailBase)args.EcmMessage, args, _logger, _cipher, _pipelineHelper, _settings).FillEmail();

            }
            else if (args.EcmMessage is MailMessageItem)
            {
                new MailMessageFillerCustom((MailMessageItem)args.EcmMessage, args, _cipher, _logger, _pipelineHelper, _settings).FillEmail();
            }
        }

        public class HtmlMailFillerCustom : HtmlMailFiller
        {
            public HtmlMailFillerCustom(HtmlMailBase message, SendMessageArgs args, ILogger logger, IStringCipher cipher, PipelineHelper pipelineHelper, EcmSettings settings) : base(message, args, logger, cipher, pipelineHelper, settings)
            {

            }

            public new void FillEmail()
            {
                base.FillEmail();
                Email.FromAddress = "newsalesperson@mail.local";
                Email.FromName = "Sales Person Name";
                Email.Headers.Add("Sender", "Sales Person Name");
            }
        }

        public class MailMessageFillerCustom : MailMessageFiller
        {
            public MailMessageFillerCustom(MailMessageItem message, SendMessageArgs args, IStringCipher cipher, ILogger logger, PipelineHelper pipelineHelper, EcmSettings settings) : base(message, args, cipher, logger, pipelineHelper, settings)
            {
            }

            public new void FillEmail()
            {
                base.FillEmail();
                Email.FromAddress = "newsalesperson@mail.local";
                Email.FromName = "Sales Person Name";
                Email.Headers.Add("Sender", "Sales Person Name");
            }
        }
    }
}
