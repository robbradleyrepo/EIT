﻿namespace LionTrust.Feature.Fund.PerformanceTables
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

            var result = new CumulativePerformanceTableViewModel { Component = datasource };
            
            if (datasource.Fund != null)
            {
                var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, datasource.Fund);
                if (!string.IsNullOrEmpty(citiCode))
                {
                    result.Rows = _performanceManager.GetPerformanceTableRows(citiCode).GroupBy(r => r.Name).Select(g => g.First()).ToArray();
                    result.QuartileRow = _performanceManager.GetQuartile(citiCode);

                    var currentClass = datasource.Fund.Classes.FirstOrDefault(c => c.CitiCode == citiCode);
                    if (currentClass != null)
                    {
                        result.Hide = currentClass.HideDiscretePerformanceTable;
                    }
                }
            }

            return View("/views/fund/performancetable.cshtml", result);
        }
    }
}