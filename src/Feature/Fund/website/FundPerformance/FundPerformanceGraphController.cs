﻿namespace LionTrust.Feature.Fund.FundPerformance
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.FundClass;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;

    public class FundPerformanceGraphController: SitecoreController
    {
        private readonly IMvcContext _context;

        public FundPerformanceGraphController(IMvcContext context)
        {
            this._context = context;
        }

        public ActionResult Render()
        {
            var datasource = _context.GetDataSourceItem<IFundPerformanceGraph>();
            if (datasource == null)
            {
                return null;
            }

            var model = new FundPerformanceGraphViewModel { Component = datasource };
            if (datasource.Fund != null)
            {
                var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, datasource.Fund);
                var currentClass = datasource.Fund.Classes.FirstOrDefault(c => c.CitiCode == citiCode);
                if (currentClass != null && currentClass.Factsheet != null)
                {
                    model.FactsheetUrl = currentClass.Factsheet.Src;
                }

                model.CitiCode = citiCode;
            }

            return View("/views/fund/fundperformancegraph.cshtml", model);
        }
    }
}