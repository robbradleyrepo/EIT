namespace LionTrust.Feature.Fund.SectorBreakdown
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Api;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;

    public class SectorBreakdownController: SitecoreController
    {
        private readonly IMvcContext _context;
        private readonly ISectorBreakdownManager _sectorBreakdownManager;

        public SectorBreakdownController(IMvcContext context, ISectorBreakdownManager sectorBreakdownManager)
        {
            this._context = context;
            this._sectorBreakdownManager = sectorBreakdownManager;
        }

        public ActionResult Render()
        {
            var datasource = _context.GetDataSourceItem<ISectorBreakdown>();
            if (datasource == null)
            {
                return null;
            }

            FundBreakdownModel[] breakdown = null;
            if (datasource.Fund != null)
            {
                breakdown = _sectorBreakdownManager.GetFundClassBreakdowns(datasource.Fund.CitiCode).ToArray();
            }

            return View("/views/fund/SectorBreakdown.cshtml", new SectorBreakdownViewModel { Breakdown = breakdown, Component = datasource });
        }
    }
}