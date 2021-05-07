namespace LionTrust.Feature.Fund.Controllers
{
    using System.Web.Mvc;

    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Models;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Mvc.Controllers;

    public class FundsController : SitecoreController
    {
        private readonly IMvcContext _context;

        public FundsController(IMvcContext context)
        {
            this._context = context;
        }

        public ActionResult FundOverview()
        {
            var viewModel = new FundOverviewViewModel();
            var fundPageData = _context.GetPageContextItem<IFundPage>();
            if (fundPageData != null && fundPageData.FundReference != null)
            {
                viewModel.FundPage = fundPageData.FundReference;
            }

            viewModel.OverviewComponent = _context.GetDataSourceItem<IFundOverview>();

            return View("~/Views/Fund/FundOverview.cshtml", viewModel);
        }
    }
}