namespace LionTrust.Feature.Fund.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Models;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;
    using System.Linq;
    using LionTrust.Foundation.Onboarding.Helpers;

    public class FundScrollerController : SitecoreController
    {
        private readonly IMvcContext context;

        public FundScrollerController(IMvcContext context)
        {
            this.context = context;
        }

        public ActionResult Render()
        {
            var datasource = context.GetDataSourceItem<IFundScroller>();
            if (datasource == null)
            {
                return null;
            }

            datasource.Funds = datasource.Funds.Where(f => OnboardingHelper.HasAccess(f.ExcludedCountries));

            return View("/views/fund/fundscroller.cshtml", datasource);
        }
    }
}