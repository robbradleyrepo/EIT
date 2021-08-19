namespace LionTrust.Feature.Fund.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Models;
    using LionTrust.Foundation.Onboarding.Helpers;
    using Sitecore.Analytics;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

    public class FundCentreController : SitecoreController
    {
        private readonly IMvcContext _context;

        public FundCentreController(IMvcContext context)
        {
            _context = context;
        }

        public ActionResult Render()
        {
            var data = _context.GetDataSourceItem<IFundCentre>();
            var country = OnboardingHelper.GetCurrentContactCountry(_context);

            if (data == null || string.IsNullOrWhiteSpace(data.FundCentreIFrameRootUrl) || country == null || string.IsNullOrWhiteSpace(country.FundCentreCountryCode))
            {
                return new EmptyResult();
            }

            data.FundCentreIFrameRootUrl = $"{data.FundCentreIFrameRootUrl}?category={country.FundCentreCountryCode}";

            return View("~/Views/Fund/fundcentre.cshtml", data);
        }
    }
}