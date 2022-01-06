namespace LionTrust.Feature.Fund.FundManagerPromo
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;

    public class FundManagerPromoController: SitecoreController
    {
        private readonly IMvcContext context;
        private readonly BaseLog _log;

        public FundManagerPromoController(IMvcContext context, BaseLog log)
        {
            this.context = context;
            this._log = log;
        }

        public ActionResult Render()
        {
            var datasource = context.GetDataSourceItem<IFundManagerPromo>();

            if (datasource == null)
            {
                _log.Info("Fund manager promo datasource is null", this);
                return null;
            }

            var currentPage = context.GetPageContextItem<IArticle>();
            var fundManagerList = new List<FundManagerViewModel>();

            //If the current page this is being used on is an article and has authors, use the authors.
            //Otherwise use the default of manually selecting using the datasource item.            
            if (currentPage != null && currentPage.Authors != null && currentPage.Authors.Any())
            {
                foreach(var author in currentPage.Authors)
                {
                    if (author != null)
                    {
                        fundManagerList.Add(new FundManagerViewModel(author));
                    }
                }
            }
            else
            {
                if (datasource.FundManager == null)
                {
                    if (!Sitecore.Context.PageMode.IsExperienceEditor)
                    {
                        _log.Info($"Fund manager promo {datasource.Id} does not have a fund manager set", this);
                        return null;
                    }

                    return View("/views/fund/FundManagerPromo.cshtml", null);
                }

                fundManagerList.Add(new FundManagerViewModel(datasource.FundManager));
            }

            return View("/views/fund/FundManagerPromo.cshtml", fundManagerList);
        }
    }
}