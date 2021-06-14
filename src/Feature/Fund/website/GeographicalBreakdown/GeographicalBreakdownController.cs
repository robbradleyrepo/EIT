namespace LionTrust.Feature.Fund.GeographicalBreakdown
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Feature.Fund.FundClass;
    using LionTrust.Feature.Fund.Models;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

    public class GeographicalBreakdownController: SitecoreController
    {
        private readonly IGeographicalBreakdownManager _manager;
        private readonly IMvcContext _context;

        public GeographicalBreakdownController(IGeographicalBreakdownManager manager, IMvcContext context)
        {
            this._manager = manager;
            this._context = context;
        }

        public ActionResult Render()
        {
            var datasource = _context.GetDataSourceItem<IGraphWithHeading>();
            if (datasource == null)
            {
                return null;
            }

            if (datasource.Fund != null)
            {
                var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, datasource.Fund);
                if (string.IsNullOrEmpty(citiCode))
                {
                    return null;
                }

                var result = _manager.GetBreakdowns(citiCode);
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return View("/views/fund/geographicalbreakdown.cshtml", new GraphBreakdownViewModel { Breakdown = result, Component = datasource });
                }                
            }

            return View("/views/fund/geographicalbreakdown.cshtml", new GraphBreakdownViewModel { Breakdown = new FundBreakdownModel[0], Component = datasource });
        }
    }
}