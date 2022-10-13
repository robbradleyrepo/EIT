namespace LionTrust.Feature.Fund.PerformanceTables
{
    using System;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.FundClass;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;
    using System.Linq;
    using Sitecore.ContentSearch.Utilities;

    public class CumulativePerformanceController : SitecoreController
    {
        private readonly ICumulativePerformanceManager _performanceManager;
        private readonly IMvcContext _context;

        private const string SinceInceptionColumnName = "Since Inception";

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

            var result = new CumulativePerformanceTableViewModel { Component = datasource };

            if (datasource.Fund == null)
            {
                return View("/views/fund/performancetable.cshtml", result);
            }

            var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, datasource.Fund);
            if (string.IsNullOrEmpty(citiCode))
            {
                return View("/views/fund/performancetable.cshtml", result);
            }

            var currentClass = datasource.Fund.Classes.FirstOrDefault(c => c.CitiCode == citiCode);
            if (currentClass != null)
            {
                result.Hide = currentClass.HideCumulativePerformanceTable;
            }
               
            result.Rows = _performanceManager.GetPerformanceTableRows(citiCode, currentClass).GroupBy(r => r.Name).Select(g => g.First()).ToArray();

            if (result.Rows != null && result.Rows.Any())
            {
                result.QuartileRow = _performanceManager.GetQuartile(citiCode, currentClass);
            }
                    
            if (currentClass?.HideSinceInceptionColumn != null && currentClass.HideSinceInceptionColumn)
            {
                var inceptionIndex = Array.IndexOf(result.ColumnHeadings, SinceInceptionColumnName);
                if (result.Rows != null && result.Rows.Any())
                {
                    foreach (var resultRow in result.Rows)
                    {
                        resultRow.Columns = resultRow.Columns.Where((source, index) => index != inceptionIndex)
                            .ToArray();
                    }
                }

                if (result.QuartileRow?.Columns != null)
                {
                    result.QuartileRow.Columns = result.QuartileRow.Columns.Where((source, index) => index != inceptionIndex).ToArray();
                }

                result.ColumnHeadings = result.ColumnHeadings.RemoveWhere(x => x.Equals(SinceInceptionColumnName))
                    .ToArray();
            }

            result.Disclaimer = _performanceManager.GetDisclaimer(citiCode, currentClass.Currency, datasource.Disclaimer);

            return View("/views/fund/performancetable.cshtml", result);
        }
    }
}