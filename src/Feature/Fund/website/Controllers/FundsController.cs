namespace LionTrust.Feature.Fund.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.FundClass;
    using LionTrust.Feature.Fund.Models;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Mvc.Controllers;

    public class FundsController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly IFundClassDetails _fundRepository;

        public FundsController(IMvcContext context, IFundClassDetails fundRepository)
        {
            this._context = context;
            this._fundRepository = fundRepository;
        }

        public ActionResult KeyInfoPrice()
        {
            var viewModel = new KeyInfoPriceViewModel();
            var data = _context.GetDataSourceItem<IKeyInfoPriceComponent>();
            var pageData = _context.GetPageContextItem<IFundSelector>();
            var fund = data != null && data.Fund != null ? data.Fund : pageData?.Fund;
            if (fund != null)
            {
                var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, fund);
                var fundClass = fund.Classes.Where(c => c.CitiCode == citiCode).FirstOrDefault();
                if (fundClass != null)
                {
                    viewModel.ClassData = _fundRepository.GetFundClassDetails(fundClass);
                }

                viewModel.Fund = fund;
            }

            viewModel.Component = data;

            return View("~/Views/Fund/KeyInfoPrice.cshtml", viewModel);
        }

        public ActionResult Disclaimer()
        {
            var viewModel = new FundDisclaimerViewModel();
            var pageData = _context.GetPageContextItem<IPresentationBase>();
            viewModel.Fund = pageData?.Fund;
            viewModel.Component = _context.GetDataSourceItem<IFundDisclaimer>();

            return View("~/Views/Fund/Disclaimer.cshtml", viewModel);
        }
    }
}