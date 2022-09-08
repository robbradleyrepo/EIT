using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.EmailCampaign.Model.Web.Settings;

namespace LionTrust.Feature.EXM.Extensions
{
    public static class MessageExtensions
    {
        public static string GetMessageUrl(this Item item)
        {
            var messageUrl = $"/?sc_itemid={item.ID}&sc_lang={item.Language.Name}&{GlobalSettings.OnlineVersionQueryStringKey}=1";
            return messageUrl;
        }

        public static string GetAbsoluteMessageUrl(this Item item)
        {
            var siteContext = Factory.GetSite(Constants.SiteName);
            var relativeUrl = item.GetMessageUrl();

            return string.Format("{0}://{1}{2}", siteContext.SiteInfo.Scheme, siteContext.TargetHostName, relativeUrl);
        }
    }
}