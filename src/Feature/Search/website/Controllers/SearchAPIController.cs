namespace LionTrust.Feature.Search.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using LionTrust.Feature.Search.DataManagers.Interfaces;
    using Sitecore.Mvc.Controllers;
    
    public class SearchAPIController : SitecoreController
    {
        private readonly IArticleSearchDataManager _articleListingDataManager;

        public SearchAPIController(IArticleSearchDataManager articleListingDataManager)
        {
            this._articleListingDataManager = articleListingDataManager;
        }

        /// <summary>
        /// Gets article facets that will be used for filtering.
        /// </summary>
        /// <param name="articleListingFacetConfig">Guid of the articleListingFacetConfig to use in multi site scenario - default is used if none set</param>
        /// <returns>A list of articles.</returns>
        public ActionResult GetArticleListingFacets(string articleListingFacetConfig)
        {
            Guid config;
            if(string.IsNullOrEmpty(articleListingFacetConfig))
            {
                config = new Guid(Constants.APIFacets.Defaults.ArticleSearchFacetsConfig);
            }
            else
            {
                var success = Guid.TryParse(articleListingFacetConfig, out config);
                if(!success)
                {
                    return Content("Configuration ID could not be parsed as a Guid");
                }
            }

            var response = _articleListingDataManager.GetArticleFilterFacets(config);
            var json = new JavaScriptSerializer().Serialize(response);

            return Content(json);
        }


        /// <summary>
        /// Gets articles based on filters in the request.
        /// </summary>
        /// <returns>A list of articles.</returns>
        public ActionResult GetFilteredArticles(string funds, string fundCategories, string fundManagers, string fundTeams, int? month, int? year, string searchTerm, int page = 1)
        {
            var response = this._articleListingDataManager.GetArticleListingResponse(funds, fundCategories, fundManagers, fundTeams, month, year, searchTerm, page);
            var json = new JavaScriptSerializer().Serialize(response);

            return Content(json);
        }
    }
}