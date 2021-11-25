namespace LionTrust.Feature.Fund.PerformanceTables
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.FundClass;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;
    using System.Linq;
    using LionTrust.Foundation.Legacy.Models;

    public class CumulativePerformanceController : SitecoreController
    {
        private readonly ICumulativePerformanceManager _performanceManager;
        private readonly IMvcContext _context;

        public CumulativePerformanceController(ICumulativePerformanceManager performanceManager, IMvcContext context)
        {
            this._performanceManager = performanceManager;
            this._context = context;
        }

        public ActionResult Render()
        {
            var datasource = _context.GetDataSourceItem<IPerformanceTable>();
            if (datasource == null)
            {
                return null;
            }

            bool hideCumulativeTable = false;
            var fundDetailPage = _context.GetContextItem<IPresentationBase>();
            if (fundDetailPage != null)
            {
                hideCumulativeTable = fundDetailPage.HideCumulativePerformanceTable;
            }

            var result = new CumulativePerformanceTableViewModel { Component = datasource, Hide = hideCumulativeTable };
            
            if (datasource.Fund != null)
            {
                var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, datasource.Fund);
                if (!string.IsNullOrEmpty(citiCode))
                {
                    result.Rows = _performanceManager.GetPerformanceTableRows(citiCode).GroupBy(r => r.Name).Select(g => g.First()).ToArray();
                    result.QuartileRow = _performanceManager.GetQuartile(citiCode);
                }
            }

            return View("/views/fund/performancetable.cshtml", result);
        }
    }
}