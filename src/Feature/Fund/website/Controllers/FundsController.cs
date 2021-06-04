namespace LionTrust.Feature.Fund.Controllers
{
    using System.Web.Mvc;
    using Glass.Mapper.Sc;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Models;
    using LionTrust.Feature.Fund.Repository;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Mvc.Controllers;

    public class FundsController : SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly IFundRepository _fundRepository;
        private readonly ISitecoreService _service;

        public FundsController(IMvcContext context, IFundRepository fundRepository, ISitecoreService service)
        {
            this._context = context;
            this._fundRepository = fundRepository;
            this._service = service;
        }

        public ActionResult KeyInfoPrice()
        {
            var viewModel = new KeyInfoPriceViewModel();
            var data = _context.GetDataSourceItem<IKeyInfoPriceComponent>();
            if (data != null && data.FundClass != null)
            {
                var fund = _fundRepository.GetFundByClass(data.FundClass, _service.Database.Name);
                if (fund != null)
                {
                    viewModel.Fund = _service.GetItem<IFund>(new GetItemByIdOptions(fund.ItemId.Guid));
                }
            }

            viewModel.Component = data;
            
            return View("~/Views/Fund/KeyInfoPrice.cshtml", viewModel);
        }

        public ActionResult AdditionalInfoAndCharges()
        {
            var viewModel = new AdditionalInfoAndChargesViewModel();
            var data = _context.GetDataSourceItem<IAdditionalInfoAndChargesComponent>();
            viewModel.Component = data;
            if (data != null && data.FundClass != null)
            {
                var fund = _fundRepository.GetFundByClass(data.FundClass, _service.Database.Name);
                if (fund != null)
                {
                    viewModel.Fund = _service.GetItem<IFund>(new GetItemByIdOptions(fund.ItemId.Guid));
                }
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