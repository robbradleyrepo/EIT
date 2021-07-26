namespace LionTrust.Feature.Search.Controllers
{
    using System;
    using System.Web.Mvc;
    using LionTrust.Feature.Search.DataManagers.Interfaces;
    using LionTrust.Foundation.Contact.Services;
    using Sitecore.Analytics;
    using LionTrust.Foundation.Core.ActionResults;  
    using Sitecore.Mvc.Controllers;
    using System.Linq;

    public class SearchAPIController : SitecoreController
    {
        private readonly IArticleSearchDataManager _articleListingDataManager;
        private readonly IFundSearchDataManager _fundListingDataManager;
        private readonly IContactService _contactService;
        private readonly ISiteSearchDataManager _siteSearchDataManager;

        public SearchAPIController(IArticleSearchDataManager articleListingDataManager, IFundSearchDataManager fundListingDataManager, IContactService contactService, ISiteSearchDataManager siteSearchDataManager)
        {
            this._articleListingDataManager = articleListingDataManager;
            this._fundListingDataManager = fundListingDataManager;
            this._contactService = contactService;
            this._siteSearchDataManager = siteSearchDataManager;
        }

        /// <summary>
        /// Gets article facets that will be used for filtering.
        /// </summary>
        /// <param name="articleListingFacetConfig">Guid of the articleListingFacetConfig to use in multi site scenario - default is used if none set</param>
        /// <returns>A list of articles.</returns>        
        public ActionResult GetArticleListingFacets(string articleListingFacetConfig)
        {
            Guid config;
            if (string.IsNullOrEmpty(articleListingFacetConfig))
            {
                config = new Guid(Search.Constants.APIFacets.Defaults.ArticleSearchFacetsConfig);
            }
            else
            {
                var success = Guid.TryParse(articleListingFacetConfig, out config);
                if (!success)
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
        public ActionResult GetFilteredArticles(string contentType, string funds, string categories, string fundManagers, string fundTeams, int? month, int? year, string searchTerm, string sortOrder, string database = "web", int page = 1)
        {
            var response = this._articleListingDataManager.GetArticleListingResponse(database, contentType, funds, categories, fundManagers, fundTeams, month, year, searchTerm, sortOrder, page);
            if (response.StatusCode != 200)
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
                config = new Guid(Search.Constants.APIFacets.Defaults.FundSearchFacetsConfig);
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
        /// Gets funds based on filters in the request.
        /// </summary>
        /// <returns>A list of funds.</returns>
        public ActionResult GetFilteredFunds(string fundTeams, string fundManagers, string fundRegions, string fundRanges, string searchTerm, string sortOrder, string database = "web", int page = 1)
        {
            var response = _fundListingDataManager.GetFundListingResponse(database, fundTeams, fundManagers, fundRegions, fundRanges, searchTerm, sortOrder, page);
            if (response.StatusCode != 200)
            {
                return new HttpStatusCodeResult(response.StatusCode, response.StatusMessage);
            }
            else if(response.TotalResults > 0 && response.SearchResults != null)
            {
                var fundUpdateArticles = _articleListingDataManager.GetArticleListingResponse(
                                            database, 
                                            Search.Constants.APIFacets.Defaults.FundUpdateContentTypeId, 
                                            string.Join("|", response.SearchResults.Select(f => f.FundId.ToString("N"))),
                                            null,
                                            fundManagers,
                                            fundTeams, 
                                            null, 
                                            null,
                                            searchTerm,
                                            sortOrder,
                                            page,
                                            int.MaxValue);
                var funds = response.SearchResults.ToList();

                if (fundUpdateArticles != null && fundUpdateArticles.TotalResults > 0)
                {
                    foreach (var fund in funds)
                    {
                        var latestFundUpdateArticle = fundUpdateArticles.SearchResults.FirstOrDefault(a => a.FundId == fund.FundId.ToString("N"));
                        if (latestFundUpdateArticle != null && !string.IsNullOrWhiteSpace(latestFundUpdateArticle.Url))
                        {
                            fund.FundUpdateUrl = latestFundUpdateArticle.Url;
                        }
                    }

                    response.SearchResults = funds;
                }
            }

            return new JsonCamelCaseResult(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets funds that a user is following.
        /// </summary>
        /// <returns>A list of funds.</returns>
        public ActionResult GetMyFilteredFunds(string fundTeams, string sortOrder, string database = "web", int page = 1)
        {
            if (Tracker.Current == null || Tracker.Current.Contact == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }

            var contactFacetData = _contactService.GetCurrentSitecoreContactFacetData(Tracker.Current.Contact.ContactId.ToString());

            if (contactFacetData == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }

            var funds = string.Join("|", contactFacetData.SalesforceFundIds);
            var response = this._fundListingDataManager.GetMyFundListingResponse(database, fundTeams, funds, sortOrder, page);

            if (response.StatusCode != 200)
            {
                return new HttpStatusCodeResult(response.StatusCode, response.StatusMessage);
            }

            return new JsonCamelCaseResult(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets pages based on site search in the request.
        /// </summary>
        /// <returns>A list of site search results.</returns>
        public ActionResult GetFilteredSearch(string query, string filters, string database = "web", int page = 1, int take = 21)
        {
            var response = _siteSearchDataManager.Search(query, database, filters, Sitecore.Context.Language.Name, take, page);
            if (response.StatusCode != 200)
            {
                return new HttpStatusCodeResult(response.StatusCode, response.StatusMessage);
            }

            return new JsonCamelCaseResult(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSiteSearchFacets(string facetConfig)
        {
            Guid config;
            if (string.IsNullOrEmpty(facetConfig))
            {
                config = new Guid(Search.Constants.APIFacets.Defaults.SiteSearchFacetsConfig);
            }
            else
            {
                var success = Guid.TryParse(facetConfig, out config);
                if (!success)
                {
                    return Content("Configuration ID could not be parsed as a Guid");
                }
            }

            var response = _siteSearchDataManager.GetFilterFacets(config);
            if (response == null)
            {
                return new HttpNotFoundResult();
            }

            return new JsonCamelCaseResult(response, JsonRequestBehavior.AllowGet);
        }
    }
}