namespace LionTrust.Feature.Carousel.Controllers
{
    using Glass.Mapper.Sc;
    using Glass.Mapper.Sc.Web;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Carousel.Models;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

    public class FundScrollerController: SitecoreController
    {
        private readonly IMvcContext context;

        public FundScrollerController(IMvcContext context)
        {
            this.context = context;
        }

        public ActionResult Render()
        {
            var datasource = context.GetDataSourceItem<IFundScroller>();
            if (datasource == null)
            {
                return null;
            }

            return View();
        }
    }
}