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
    using Sitecore.Globalization;
    using Sitecore.Resources.Media;

    public class ArticleSearchDataManager : IArticleSearchDataManager
    {
        private readonly IArticleContentSearchService _articleContentSearchService;
        private readonly IContentRepository _contentRepository;

        public ArticleSearchDataManager(IArticleContentSearchService articleContentSearchService, IContentRepository contentRepository)
        {
            _articleContentSearchService = articleContentSearchService;
            _contentRepository = contentRepository;
        }

        private string GetArticleDate(DateTime indexedDate)
        {
            var label = string.Empty;
            if(indexedDate.Date == DateTime.Today)
            {
                label = Translate.Text("Today");
            }
            else if (DateTime.Today - indexedDate.Date == TimeSpan.FromDays(1))
            {
                label = Translate.Text("Yesterday");
            }

            if (string.IsNullOrEmpty(label))
            {
                label = indexedDate.ToString("D", Sitecore.Context.Culture);
            }

            return label;
        }

        private IEnumerable<ITaxonomyContentResult> MapArticleResultHits(IEnumerable<SearchHit<ArticleSearchResultItem>> hits)
        {
            return hits.Select(x => new ArticleResult{
                                                        Authors = x.Document.ArticleAuthorNames?.Split('|'),
                                                        Category = x.Document.ArticleCategoryTagName,
                                                        Date = this.GetArticleDate(x.Document.ArticleDate),
                                                        Fund = x.Document.ArticleFundName,
                                                        ImageUrl = x.Document.ArticleListingImage,
                                                        Subtitle = x.Document.ArticleSubtitle,
                                                        Team = x.Document.ArticleTeam,
                                                        Title = x.Document.ArticleTitle,
                                                        Topics = x.Document.TopicNames?.Split('|'),
                                                        AuthorImageUrl = x.Document.ArticleAuthorImage
                                                     });
        }

        public ArticleFacetsResponse GetArticleFilterFacets(Guid articleFilterFacetConfigId)
        {
            var filterFacetConfigItem = _contentRepository.GetItem<IArticleListingFacetsConfig>(new GetItemByIdOptions(articleFilterFacetConfigId));

            var listingArticleFacetsResponse = new ArticleFacetsResponse();
            if(filterFacetConfigItem == null 
                    || filterFacetConfigItem.FundsFolder == null
                    || filterFacetConfigItem.FundCategoriesFolder == null
                    || filterFacetConfigItem.FundManagersFolder == null
                    || filterFacetConfigItem.FundTeamsFolder == null)
            {
                return null;
            }

            listingArticleFacetsResponse.Facets = new ArticleFacets {
                                                    Funds = filterFacetConfigItem.FundsFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString("N"), Name = x.Name }),
                                                    FundCategories = filterFacetConfigItem.FundCategoriesFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString("N"), Name = x.Name }),
                                                    FundManagers = filterFacetConfigItem.FundManagersFolder?.Children?.Where(x => x.IsFundManager)?.Select(x => new FacetItem { Identifier = x.Id.ToString("N"), Name = x.Name }),
                                                    FundTeams = filterFacetConfigItem.FundTeamsFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString("N"), Name = x.Name })
                                                  };

            return listingArticleFacetsResponse;
        }

        public ITaxonomySearchResponse GetArticleListingResponse(string database, string funds, string fundCategories, string fundManagers, string fundTeams, int? month, int? year, string searchTerm, string sortOrder, int page)
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
                Skip = page * 21,
                Take = 21,
                ToDate = new DateTime(toYear, toMonth, DateTime.DaysInMonth(toYear, toMonth))
            };

            ContentSearchResults contentSearchResults;
            if (sortOrder == "ASC")
            {
                contentSearchResults = this._articleContentSearchService.GetDatedTaxonomyRelatedArticles(articleSearchRequest, result => result.OrderBy(x => x.ArticleDate));
            }
            else if (sortOrder == "DESC")
            {
                contentSearchResults = this._articleContentSearchService.GetDatedTaxonomyRelatedArticles(articleSearchRequest, result => result.OrderByDescending(x => x.ArticleDate));
            }
            else
            {
                contentSearchResults = this._articleContentSearchService.GetDatedTaxonomyRelatedArticles(articleSearchRequest);
            }

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