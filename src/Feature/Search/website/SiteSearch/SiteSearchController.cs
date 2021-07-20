namespace LionTrust.Feature.Search.SiteSearch
{
    using Glass.Mapper.Sc.Web.Mvc;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

    public class SiteSearchController : SitecoreController
    {
        private readonly IMvcContext _context;

        public SiteSearchController(IMvcContext context)
        {
            this._context = context;
        }

        public ActionResult Render()
        {
            var datasource = _context.GetDataSourceItem<ISiteSearch>();
            if (datasource == null)
            {
                return null;
            }

            return View("/views/search/searchresults.cshtml", datasource);
        }
    }
}