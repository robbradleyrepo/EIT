namespace LionTrust.Feature.Search.SearchOverlay
{
    using Glass.Mapper.Sc.Web.Mvc;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;

    public class SearchOverlayController: SitecoreController
    {
        private readonly IMvcContext _context;

        public SearchOverlayController(IMvcContext context)
        {
            this._context = context;
        }

        public ActionResult Render()
        {
            var datasource = _context.GetDataSourceItem<ISearchOverlay>();
            if (datasource == null || datasource.SearchResultsPage == null)
            {
                return null;
            }

            return View("/views/search/searchoverlay.cshtml", datasource);
        }
    }
}