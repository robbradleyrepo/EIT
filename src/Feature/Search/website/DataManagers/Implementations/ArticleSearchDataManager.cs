namespace LionTrust.Feature.Search.DataManagers.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Glass.Mapper.Sc;
    using LionTrust.Feature.Search.DataManagers.Interfaces;
    using LionTrust.Feature.Search.Models.API;
    using LionTrust.Feature.Search.Models.API.Facets;
    using LionTrust.Feature.Search.Models.API.Response;
    using LionTrust.Foundation.Content.Repositories;
    using LionTrust.Foundation.Search.Models;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Models.Response;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.ContentSearch.Linq;

    public class ArticleSearchDataManager : IArticleSearchDataManager
    {
        private readonly IArticleContentSearchService _articleContentSearchService;
        private readonly IContentRepository _contentRepository;

        public ArticleSearchDataManager(IArticleContentSearchService articleContentSearchService, IContentRepository contentRepository)
        {
            _articleContentSearchService = articleContentSearchService;
            _contentRepository = contentRepository;
        }

        private IEnumerable<ITaxonomyContentResult> MapArticleResultHits(IEnumerable<SearchHit<ArticleSearchResultItem>> hits)
        {
            return hits.Select(x => new ArticleResult{
                                                        Authors = x.Document.ArticleAuthors,
                                                        Category = x.Document.ArticleCategory,
                                                        Content = x.Document.ArticleContent,
                                                        Date = x.Document.ArticleDate,
                                                        Fund = x.Document.ArticleFund,
                                                        Subtitle = x.Document.ArticleSubtitle,
                                                        Team = x.Document.ArticleTeam,
                                                        Title = x.Document.ArticleTitle
                                                     });
        }

        public ArticleFacetsResponse GetArticleFilterFacets(Guid articleFilterFacetConfigId)
        {
            var filterFacetConfigItem = _contentRepository.GetItem<IArticleListingFacetsConfig>(new GetItemByIdOptions(articleFilterFacetConfigId));

            var listingArticleFacetsResponse = new ArticleFacetsResponse();
            if(filterFacetConfigItem == null || filterFacetConfigItem.FundsFolder == null
                    || filterFacetConfigItem.FundCategoriesFolder == null
                    || filterFacetConfigItem.FundManagersFolder == null
                    || filterFacetConfigItem.FundTeamsFolder == null)
            {
                return null;
            }

            listingArticleFacetsResponse.FundFacets = filterFacetConfigItem.FundsFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString(), Name = x.Name });
            listingArticleFacetsResponse.FundCategoriesFacets = filterFacetConfigItem.FundCategoriesFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString(), Name = x.Name });
            listingArticleFacetsResponse.FundManagersFacets = filterFacetConfigItem.FundManagersFolder?.Children?.Where(x => x.IsFundManager)?.Select(x => new FacetItem { Identifier = x.Id.ToString(), Name = x.Name });
            listingArticleFacetsResponse.FundTeamsFacets = filterFacetConfigItem.FundTeamsFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString(), Name = x.Name });
            listingArticleFacetsResponse.FundTeamsFacets = filterFacetConfigItem.FundTeamsFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString(), Name = x.Name });
            return listingArticleFacetsResponse;
        }

        public ITaxonomySearchResponse GetArticleListingResponse(string database, string funds, string fundCategories, string fundManagers, string fundTeams, int? month, int? year, string searchTerm, int page)
        {
            var fromYear = year ?? 2000;
            var fromMonth = month ?? 1;
            var toYear = year ?? DateTime.Now.Year + 1;
            var toMonth = month ?? 12;
            page = page - 1;

            var articleSearchRequest = new ArticleSearchRequest
            {
                DatabaseName = database,
                FromDate = new DateTime(fromYear, fromMonth, 1),
                Funds = funds?.Split('|'),
                FundCategories = fundCategories?.Split('|'),
                FundManagers = fundManagers?.Split('|'),
                FundTeams = fundTeams?.Split('|'),
                SearchTerm = searchTerm,
                Skip = page * 10,
                Take = 10,
                ToDate = new DateTime(toYear, toMonth, 31)
            };

            var contentSearchResults = this._articleContentSearchService.GetDatedTaxonomyRelatedArticles(articleSearchRequest);

            var articleSearchResponse = new ArticleSearchResponse();
            if(contentSearchResults.TotalResults > 0)
            {
                articleSearchResponse.SearchResults = this.MapArticleResultHits(contentSearchResults.SearchResults);
                articleSearchResponse.StatusMessage = "Success";
                articleSearchResponse.StatusCode = 200;
                articleSearchResponse.TotalResults = contentSearchResults.TotalResults;
            }
            else
            {
                articleSearchResponse.StatusMessage = "No search results found";
                articleSearchResponse.StatusCode = 404;
            }

            return articleSearchResponse;
        }
    }
}