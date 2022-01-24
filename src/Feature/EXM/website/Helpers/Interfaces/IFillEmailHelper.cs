using Sitecore.EDS.Core.Dispatch;
using Sitecore.EmailCampaign.Cm.Pipelines.SendEmail;
using Sitecore.Modules.EmailCampaign.Messages;

namespace LionTrust.Feature.EXM.Helpers.Interfaces
{
    public interface IFillEmailHelper
    {
        void FillEmail(MailMessageItem mailMessageItem, SendMessageArgs sendMessageArgs, EmailMessage emailMessage);
    }
}