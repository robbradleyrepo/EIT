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
            if (data != null && data.Fund != null)
            {
                var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, data.Fund);
                var fundClass = data.Fund.Classes.Where(c => c.CitiCode == citiCode).FirstOrDefault();
                if (fundClass != null)
                {
                    viewModel.ClassData = _fundRepository.GetFundClassDetails(fundClass);
                }

                viewModel.Fund = data.Fund;
                
            }

            viewModel.Component = data;
            
            return View("~/Views/Fund/KeyInfoPrice.cshtml", viewModel);
        }

        public ActionResult AdditionalInfoAndCharges()
        {
            var viewModel = new AdditionalInfoAndChargesViewModel();
            var data = _context.GetDataSourceItem<IAdditionalInfoAndChargesComponent>();
            viewModel.Component = data;
            if (data != null && data.Fund != null)
            {
                viewModel.Fund = data.Fund;
            }

            return View("~/Views/Fund/AdditionalInfoAndCharges.cshtml", viewModel);
        }

        public ActionResult Disclaimer()
        {
            var viewModel = new FundDisclaimerViewModel();
            viewModel.Fund = _context.GetPageContextItem<IFund>();
            viewModel.Component = _context.GetDataSourceItem<IFundDisclaimer>();

            return View("~/Views/Fund/Disclaimer.cshtml", viewModel);
        }
    }
}