namespace LionTrust.Feature.Fund.FundManagerPromo
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;
    using System.Linq;

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

            //If the current page this is being used on is an article and has one author, use this author.
            //Otherwise use the default of manually selecting using the datasource item.
            //If multiple authors are configured this is not possible as everytime you add the compondent it
            //would just select the first, as instances of this compondent do not know about the others used on the page.
            if (currentPage != null && currentPage.Authors != null && currentPage.Authors.Count() == 1)
            {
                datasource.FundManager = currentPage.Authors.First();
            }

            if (!Sitecore.Context.PageMode.IsExperienceEditor)
            {
                if (datasource.FundManager == null)
                {
                    _log.Info($"Fund manager promo {datasource.Id} does not have a fund manager set", this);
                    return null;
                }
            }

            if (datasource.FundManager == null)
            {
                return View("/views/fund/FundManagerPromo.cshtml", null);
            }

            return View("/views/fund/FundManagerPromo.cshtml", new FundManagerViewModel(datasource.FundManager));
        }
    }
}