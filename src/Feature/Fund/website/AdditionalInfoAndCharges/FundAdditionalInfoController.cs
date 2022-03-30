namespace LionTrust.Feature.Fund.AdditionalInfoAndCharges
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.FundClass;
    using LionTrust.Feature.Fund.Models;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;

    public class FundAdditionalInfoController: SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly IAdditionalFundInformationManager _manager;

        public FundAdditionalInfoController(IMvcContext context, IAdditionalFundInformationManager manager)
        {
            this._context = context;
            this._manager = manager;
        }

        public ActionResult Render()
        {            
            var viewModel = new AdditionalInfoAndChargesViewModel();
            var datasource = _context.GetDataSourceItem<IAdditionalInfoAndChargesComponent>();            
            if (datasource == null)
            {
                return null;
            }

            viewModel.Component = datasource;
            var fund = datasource.Fund;
            if (fund == null)
            {
                var fundPage = _context.GetContextItem<IFundSelector>();
                if (fundPage != null)
                {
                    fund = fundPage.Fund;
                }
            }

            if (fund != null)
            {
                var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, fund);
                if (!string.IsNullOrEmpty(citiCode))
                {
                    var fundClass = fund.Classes.FirstOrDefault(c => c.CitiCode == citiCode);
                    if (fundClass != null)
                    {
                        viewModel.Data = _manager.GetAdditionalInformation(fundClass, citiCode);
                        viewModel.FundClass = fundClass;
                    }
                }
            }

            return View("~/Views/Fund/AdditionalInfoAndCharges.cshtml", viewModel);
        }

        public ActionResult RenderOnDemand()
        {
            var viewModel = new AdditionalInfoOnDemandViewModel();
            var datasource = _context.GetDataSourceItem<IAdditionalInfoOnDemandComponent>();
            if (datasource == null)
            {
                return null;
            }

            var pageData = _context.GetContextItem<IPresentationBase>();
            var fund = pageData?.Fund;
            if (fund == null)
            {
                return null;
            }

            viewModel.Component = datasource;

            var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, fund);
            if (!string.IsNullOrEmpty(citiCode))
            {
                var fundClass = fund.Classes.FirstOrDefault(c => c.CitiCode == citiCode);
                if (fundClass != null)
                {
                    viewModel.Data = _manager.GetAdditionalInformation(fundClass, citiCode);
                }
            }

            return View("~/Views/Fund/AdditionalInfoOnDemand.cshtml", viewModel);
        }
    }
}