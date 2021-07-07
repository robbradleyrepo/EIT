namespace LionTrust.Feature.Fund.FundManagerPromo
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Models;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

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
            if (!Sitecore.Context.PageMode.IsExperienceEditor)
            {
                if (datasource == null)
                {
                    _log.Info("Fund manager promo datasource is null", this);
                    return null;
                }

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