namespace LionTrust.Feature.Fund.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Models;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.Onboarding.Helpers;
    using Sitecore.Mvc.Controllers;
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

            if (data == null || hasAccess)
            {
                return new EmptyResult();
            }

            return View("~/Views/Fund/fundaccesspopup.cshtml", data);
        }
    }
}