namespace LionTrust.Feature.Article.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Services.Interfaces;

    public class ArticleRepository
    {
        private readonly IArticleContentSearchService searchService;

        public ArticleRepository(IArticleContentSearchService searchService)
        {
            this.searchService = searchService;
        }

        public IEnumerable<IArticlePromo> GetArticlesByTopics(IEnumerable<string> topics, string databaseName)
        {
            var request = new ArticleSearchRequest
            {
                Topics = topics,
                DatabaseName = databaseName,
                FromDate = DateTime.MinValue,
                ToDate = DateTime.MaxValue
            };

            var results = searchService.GetDatedTaxonomyRelatedArticles(request, result => result.OrderByDescending(hit => hit.ArticleDate));
            if (results == null || results.SearchResults == null)
            {
                return new RelatedArticle[0];
            }

            return results.SearchResults
                .Where(sr => sr.Document != null)
                .Select(sr => BuildArticle(sr.Document));
        }

        private RelatedArticle BuildArticle(ArticleSearchResultItem hit)
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

            return new RelatedArticle { Content = hit.ArticleTitle, Url = link };
        }
    }
}