namespace LionTrust.Feature.Search.Controllers
{
    using System;
    using System.Web.Mvc;
    using LionTrust.Feature.Search.ActionResults;
    using LionTrust.Feature.Search.DataManagers.Interfaces;
    using Sitecore.Mvc.Controllers;
    
    public class SearchAPIController : SitecoreController
    {
        private readonly IArticleSearchDataManager _articleListingDataManager;
        private readonly IFundSearchDataManager _fundListingDataManager;

        public SearchAPIController(IArticleSearchDataManager articleListingDataManager, IFundSearchDataManager fundListingDataManager)
        {
            this._articleListingDataManager = articleListingDataManager;
            this._fundListingDataManager = fundListingDataManager;
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
            if (response == null)
            {
                return new HttpNotFoundResult();
            }

            return new JsonCamelCaseResult(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets articles based on filters in the request.
        /// </summary>
        /// <returns>A list of articles.</returns>
        public ActionResult GetFilteredArticles(string funds, string fundCategories, string fundManagers, string fundTeams, int? month, int? year, string searchTerm, string sortOrder, string database = "web", int page = 1)
        {
            var response = this._articleListingDataManager.GetArticleListingResponse(database, funds, fundCategories, fundManagers, fundTeams, month, year, searchTerm, sortOrder, page);
            if(response.StatusCode != 200)
            {
                return new HttpStatusCodeResult(response.StatusCode, response.StatusMessage);
            }

            return new JsonCamelCaseResult(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets fund facets that will be used for filtering.
        /// </summary>
        /// <param name="fundListingFacetConfig">Guid of the fundListingFacetConfig to use in multi site scenario - default is used if none set</param>
        /// <returns>A list of funds.</returns>        
        public ActionResult GetFundListingFacets(string fundListingFacetConfig)
        {
            Guid config;
            if (string.IsNullOrEmpty(fundListingFacetConfig))
            {
                config = new Guid(Constants.APIFacets.Defaults.FundSearchFacetsConfig);
            }
            else
            {
                var success = Guid.TryParse(fundListingFacetConfig, out config);
                if (!success)
                {
                    return Content("Configuration ID could not be parsed as a Guid");
                }
            }

            var response = _fundListingDataManager.GetFundFilterFacets(config);
            if (response == null)
            {
                return new HttpNotFoundResult();
            }

            return new JsonCamelCaseResult(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets articles based on filters in the request.
        /// </summary>
        /// <returns>A list of articles.</returns>
        public ActionResult GetFilteredFunds(string fundTeams, string fundManagers, string fundRegions, string fundRanges, string searchTerm, string sortOrder, string database = "web", int page = 1)
        {
            var response = this._fundListingDataManager.GetFundListingResponse(database, fundTeams, fundManagers, fundRegions, fundRanges, searchTerm, sortOrder, page);
            if (response.StatusCode != 200)
            {
                return new HttpStatusCodeResult(response.StatusCode, response.StatusMessage);
            }

            return new JsonCamelCaseResult(response, JsonRequestBehavior.AllowGet);
        }
    }
}