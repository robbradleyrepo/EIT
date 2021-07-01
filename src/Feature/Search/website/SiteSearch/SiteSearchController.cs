namespace LionTrust.Feature.Search.SiteSearch
{
    using Glass.Mapper.Sc;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Search.DataManagers.Interfaces;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;

    public class SiteSearchController: SitecoreController
    {
        private readonly ISiteSearchDataManager _dataManager;
        private readonly ISitecoreService _service;
        private readonly IMvcContext _context;

        public SiteSearchController(ISiteSearchDataManager dataManager, ISitecoreService service, IMvcContext context)
        {
            this._dataManager = dataManager;
            this._service = service;
            this._context = context;
        }

        public ActionResult Render()
        {
            var datasource = _context.GetDataSourceItem<ISiteSearch>();
            if (datasource == null)
            {
                return null;
            }

            var searchQuery = this.Request.QueryString.Get("query");
            if (string.IsNullOrEmpty(searchQuery))
            {
                return null;
            }
            if (!int.TryParse(datasource.ResultsPerPage, out int resultsPerPage))
            {
                resultsPerPage = 10;
            }

            var results = _dataManager.Search(searchQuery, _service.Database.Name, Sitecore.Context.Language.Name, resultsPerPage, 1);

            var model = new SiteSearchViewModel { Configuration = datasource };
            if (results != null && results.Hits != null)
            {
                model.Results = results.Hits.Select(h => new SiteSearchHit { Url = h.PageUrl, Copy = h.Copy, PageTitle = h.PageTitle, Author = h.Authors, AuthorImage = h.AuthorImageUrl, FundTeam = h.FundTeamName, FundTeamUrl = h.FundTeamPage, ResultType = h.ResultType, PageDate = h.Updated });
                model.Query = searchQuery;
                model.TotalResults = results.Results;
                return View("/views/search/searchresults.cshtml", model);
            }

            return null;
        }
    }
}