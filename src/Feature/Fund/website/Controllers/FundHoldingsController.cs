namespace LionTrust.Feature.Fund.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Feature.Fund.Models;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

    public class FundHoldingsController: SitecoreController
    {
        private readonly IFundHoldingsBreakdown _dataManager;
        private readonly IMvcContext _context;

        public FundHoldingsController(IFundHoldingsBreakdown dataManager, IMvcContext context)
        {
            this._dataManager = dataManager;
            this._context = context;
        }

        public ActionResult Render()
        {
            var data = _context.GetDataSourceItem<IHoldingsTable>();
            if (data == null)
            {
                return null;
            }

            if (data.Fund == null || string.IsNullOrEmpty(data.Fund.CitiCode))
            {
                if (!Sitecore.Context.PageMode.IsExperienceEditor)
                {
                    return null;
                }                
            }

            var result = new HoldingsViewModel { Table = data };
            if (data.Fund != null && !string.IsNullOrEmpty(data.Fund.CitiCode))
            {
                result.Holdings = _dataManager.GetFundHoldings(data.Fund.CitiCode);
            }
            else
            {
                result.Holdings = new FundBreakdownModel[0];
            }

            return PartialView("/views/fund/holdings.cshtml", result);
        }
    }
}