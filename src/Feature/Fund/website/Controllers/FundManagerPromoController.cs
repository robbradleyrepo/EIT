namespace LionTrust.Feature.Fund.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Models;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

    public class FundManagerPromoController: SitecoreController
    {
        private readonly IMvcContext context;

        public FundManagerPromoController(IMvcContext context)
        {
            this.context = context;
        }

        public ActionResult Render()
        {
            var fundManagerPage = context.GetDataSourceItem<IFundManagerPage>();
            if (fundManagerPage.Manager == null && !Sitecore.Context.PageMode.IsExperienceEditor)
            {
                return null;
            }

            return View("/views/fund/FundManagerPromo.cshtml", new FundManagerViewModel(fundManagerPage));
        }
    }
}