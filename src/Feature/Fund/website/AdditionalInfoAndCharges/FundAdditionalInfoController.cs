namespace LionTrust.Feature.Fund.AdditionalInfoAndCharges
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Feature.Fund.FundClass;
    using LionTrust.Feature.Fund.Models;
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
            
            viewModel.Component = datasource;
            if (datasource != null && datasource.Fund != null)
            {
                var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, datasource.Fund);
                if (!string.IsNullOrEmpty(citiCode))
                {
                    var fundClass = datasource.Fund.Classes.FirstOrDefault(c => c.CitiCode == citiCode);
                    if (fundClass != null)
                    {
                        viewModel.Data = _manager.GetAdditionalInformation(fundClass, citiCode);
                        viewModel.FundClass = fundClass;
                    }
                }
            }

            return View("~/Views/Fund/AdditionalInfoAndCharges.cshtml", viewModel);
        }
    }
}