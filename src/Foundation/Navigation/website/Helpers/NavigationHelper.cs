namespace LionTrust.Foundation.Navigation.Helpers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Foundation.Navigation.Models;
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Onboarding.Models;
    using LionTrust.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Abstractions;
    using Sitecore.Data;
    using Sitecore.Data.Items;

    public class NavigationHelper
    {
        public static IHeaderConfiguration GetCurrentHeaderConfiguration(IMvcContext mvcContext, IOnboardingConfiguration configuration, BaseLog log)
        {
            var investor = OnboardingHelper.GetCurrentContactInvestor(mvcContext, log);

            if (investor == null || investor.Header == null)
            {
                return null;
            }

            return mvcContext.SitecoreService.GetItem<IHeaderConfiguration>(investor.Header.Id);
        }

        public static IIdentity GetWebsiteIdentity(IMvcContext mvcContext, Item contextItem)
        {
            var identityItem = contextItem.GetAncestorOrSelfOfTemplate(new ID(Constants.Identity.TemplateID));
            return (identityItem != null ? mvcContext.SitecoreService.GetItem<IIdentity>(identityItem.ID.Guid) : null);
        }
    }
}