namespace LionTrust.Feature.Fund.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Models;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.Onboarding.Helpers;
    using Sitecore.Mvc.Controllers;
    using Sitecore.Web;
    using System.Web.Mvc;

    public class FundAccessController : SitecoreController
    {
        private readonly IMvcContext _context;

        public FundAccessController(IMvcContext context)
        {
            _context = context;
        }

        public ActionResult Render()
        {
            var data = _context.GetDataSourceItem<IFundAccessPopUp>();
            var pageItem = _context.GetContextItem<IPresentationBase>();

            var hasAccess = OnboardingHelper.HasAccess(pageItem?.Fund?.ExcludedCountries);

            //Don't display the access popup if the user has access or is changing the onboarding settings.
            if (data == null || hasAccess || IsChangingOnboarding())
            {
                return new EmptyResult();
            }

            var backUrl = OnboardingHelper.GetChangeUrl();

            if (IsInternalReferrerAndNotSelf())
            {
                backUrl = "javascript:history.back()";
            }

            data.BackUrl = backUrl;

            return View("~/Views/Fund/fundaccesspopup.cshtml", data);
        }

        private bool IsChangingOnboarding()
        {
            var change = WebUtil.GetQueryString(Foundation.Onboarding.Constants.QueryStringNames.Change);
            return !string.IsNullOrEmpty(change) && change == bool.TrueString.ToLower();
        }

        private bool IsInternalReferrerAndNotSelf()
        {
            //logic to work out if the current request was from an internal url and not self eg. onboarding screen.
            return Request.UrlReferrer != null && !string.IsNullOrWhiteSpace(Request.UrlReferrer.Host) 
                && Request.Url.Host == Request.UrlReferrer.Host 
                && Request.Url != Request.UrlReferrer;
        }
    }
}