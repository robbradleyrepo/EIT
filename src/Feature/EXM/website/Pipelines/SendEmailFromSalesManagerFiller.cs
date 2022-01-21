namespace LionTrust.Feature.EXM.Pipelines
{
    using LionTrust.Feature.EXM.Helpers.Interfaces;
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
        private readonly IFillEmailHelper _fillEmailHelper;

        public SendEmailFromSalesManagerFiller(IStringCipher cipher, ILogger logger, PipelineHelper pipelineHelper, EcmSettings settings, IFillEmailHelper fillEmailHelper)
        {
            _cipher = cipher;
            _logger = logger;
            _pipelineHelper = pipelineHelper;
            _settings = settings;
            _fillEmailHelper = fillEmailHelper;
        }

        public void Process(SendMessageArgs args)
        {
            if (args.EcmMessage == null)
                return;

            if (args.EcmMessage is HtmlMailBase)
            {
                new HtmlMailFillerCustom((HtmlMailBase)args.EcmMessage, args, _logger, _cipher, _pipelineHelper, _settings, _fillEmailHelper).FillEmail();

            }
            else if (args.EcmMessage is MailMessageItem)
            {
                new MailMessageFillerCustom((MailMessageItem)args.EcmMessage, args, _cipher, _logger, _pipelineHelper, _settings, _fillEmailHelper).FillEmail();
            }
        }

        public class HtmlMailFillerCustom : HtmlMailFiller
        {
            private readonly MailMessageItem _mailMessageBase;
            private readonly SendMessageArgs _sendMessageArgs;
            private readonly IFillEmailHelper _fillEmailHelper;

            public HtmlMailFillerCustom(HtmlMailBase message, SendMessageArgs args, ILogger logger, IStringCipher cipher, PipelineHelper pipelineHelper, EcmSettings settings, IFillEmailHelper fillMailHelper) :
                base(message, args, logger, cipher, pipelineHelper, settings)
            {
                _mailMessageBase = message;
                _sendMessageArgs = args;
                _fillEmailHelper = fillMailHelper;
            }

            public new void FillEmail()
            {
                base.FillEmail();
                _fillEmailHelper.FillEmail(_mailMessageBase, _sendMessageArgs, Email);
            }
        }
    }

    public class MailMessageFillerCustom : MailMessageFiller
    {
        private readonly MailMessageItem _mailMessageBase;
        private readonly SendMessageArgs _sendMessageArgs;
        private readonly IFillEmailHelper _fillEmailHelper;

        public MailMessageFillerCustom(MailMessageItem message, SendMessageArgs args, IStringCipher cipher, ILogger logger, PipelineHelper pipelineHelper, EcmSettings settings, IFillEmailHelper fillEmailHelper) :
            base(message, args, cipher, logger, pipelineHelper, settings)
        {
            _mailMessageBase = message;
            _sendMessageArgs = args;
            _fillEmailHelper = fillEmailHelper;
        }

        public new void FillEmail()
        {
            base.FillEmail();
            _fillEmailHelper.FillEmail(_mailMessageBase, _sendMessageArgs, Email);
        }
    }
}