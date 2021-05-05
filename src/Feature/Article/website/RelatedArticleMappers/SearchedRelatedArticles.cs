namespace LionTrust.Feature.Article.RelatedArticleMappers
{
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Search.Models.API.Request;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SearchedRelatedArticles
    {
        private readonly IArticleContentSearchService searchService;

        public SearchedRelatedArticles(IArticleContentSearchService searchService)
        {
            this.searchService = searchService;
        }

        public IEnumerable<RelatedArticle> Map(IArticleFilter filter, string databaseName)
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

            var results = searchService.GetDatedTaxonomyRelatedArticles(request);
            if (results == null || results.SearchResults == null)
            {
                return new RelatedArticle[0];
            }

            return results.SearchResults
                .Where(sr => sr.Document != null)
                .Select(sr => new RelatedArticle { Url = sr.Document.Url, Content = sr.Document.ArticleTitle });
        }
    }
}