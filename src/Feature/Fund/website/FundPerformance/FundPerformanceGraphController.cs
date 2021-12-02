﻿namespace LionTrust.Feature.Fund.FundPerformance
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.FundClass;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Mvc.Controllers;
    using System;
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

            bool hidePerformanceChart = false;
            var fundDetailPage = _context.GetContextItem<IPresentationBase>();
            if (fundDetailPage != null)
            {
                hidePerformanceChart = fundDetailPage.HidePerformanceChart;
            }

            var model = new FundPerformanceGraphViewModel { Component = datasource, Hide = hidePerformanceChart };
            if (datasource.Fund != null)
            {
                var citiCode = FundClassSwitcherHelper.GetCitiCode(HttpContext, datasource.Fund);
                var currentClass = datasource.Fund.Classes.FirstOrDefault(c => c.CitiCode == citiCode);
                if (currentClass != null)
                {
                    if (currentClass.Factsheet != null)
                    {
                        model.FactsheetUrl = currentClass.Factsheet.Src;
                    }
                    
                    if (currentClass.GraphStartDate != null && currentClass.GraphStartDate != DateTime.MinValue)
                    {
                        model.StartDate = currentClass.GraphStartDate.ToString("dd-MM-yyyy");
                    }                    
                }

                model.CitiCode = citiCode;
                model.FundId = datasource.Fund.Id.ToString("N");
            }

            return View("/views/fund/fundperformancegraph.cshtml", model);
        }
    }
}