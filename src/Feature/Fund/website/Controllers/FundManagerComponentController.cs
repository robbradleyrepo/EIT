namespace LionTrust.Feature.Fund.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Fund.Models;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

    public class FundManagerComponentController : SitecoreController
    {
        private readonly IMvcContext context;

        public FundManagerComponentController(IMvcContext context)
        {
            this.context = context;
        }

        public ActionResult Render()
        {
            var datasource = context.GetDataSourceItem<IFundManagers>();
            if (datasource == null)
            {
                return null;
            }

            return View("/views/fund/FundManagerComponent.cshtml", datasource);
        }
    }
}