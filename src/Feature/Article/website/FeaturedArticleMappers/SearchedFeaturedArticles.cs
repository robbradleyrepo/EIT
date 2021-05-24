namespace LionTrust.Feature.Article.FeaturedArticleMappers
{
    using LionTrust.Feature.Article.Models;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SearchedRelatedArticles
    {
        private readonly IArticleContentSearchService searchService;
        private readonly BaseLinkManager linkManager;

        public SearchedRelatedArticles(IArticleContentSearchService searchService, BaseLinkManager linkManager)
        {
            this.searchService = searchService;
            this.linkManager = linkManager;
        }

        public IEnumerable<FeaturedArticle> Map(IArticleFilter filter, string databaseName)
        {
            var request = new ArticleSearchRequest
            {
                Funds = filter.Funds?.Select(f => f.Id.ToString().Replace("-", string.Empty)),
                FundCategories = filter.FundCategories?.Select(fc => fc.Id.ToString().Replace("-", string.Empty)),
                FundTeams = filter.FundTeam?.Select(ft => ft.Id.ToString().Replace("-", string.Empty)),
                FundManagers = filter.FundManagers?.Select(fm => fm.Id.ToString().Replace("-", string.Empty)),
                Take = 6,
                DatabaseName = databaseName,
                FromDate = DateTime.MinValue,
                ToDate = DateTime.MaxValue
            };

            var results = searchService.GetDatedTaxonomyRelatedArticles(request, result => result.OrderByDescending(hit => hit.Created));
            if (results == null || results.SearchResults == null)
            {
                return new FeaturedArticle[0];
            }

            return results.SearchResults
                .Where(sr => sr.Document != null)
                .Select(sr => BuildArticle(sr.Document));
        }

        private FeaturedArticle BuildArticle(ArticleSearchResultItem hit)
        {
            if (hit == null)
            {
                return null;
            }

            var item = hit.GetItem();
            if (item == null)
            {
                return null;
            }
            var link = linkManager.GetItemUrl(item);

            return new FeaturedArticle { Content = hit.ArticleTitle, Url = link };            
        }
    }
}