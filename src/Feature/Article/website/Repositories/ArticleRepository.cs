namespace LionTrust.Feature.Article.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Services.Interfaces;

    public class ArticleRepository
    {
        private readonly IArticleContentSearchService _searchService;
        private readonly IMvcContext _mvcContext;

        public ArticleRepository(IArticleContentSearchService searchService, IMvcContext mvcContext)
        {
            this._searchService = searchService;
            this._mvcContext = mvcContext;
        }

        public IEnumerable<IArticlePromo> GetArticlePromosByTopics(IEnumerable<string> topics)
        {
            var request = new ArticleSearchRequest
            {
                Topics = topics,
                DatabaseName = _mvcContext.SitecoreService.Database.Name,
                FromDate = DateTime.MinValue,
                ToDate = DateTime.MaxValue
            };

            var results = _searchService.GetDatedTaxonomyRelatedArticles(request, result => result.OrderByDescending(hit => hit.ArticleDate));
            if (results == null || results.SearchResults == null)
            {
                return null;
            }

            return results.SearchResults
                .Where(sr => sr.Document != null)
                .Select(sr => BuildArticle(sr.Document));
        }

        private IArticlePromo BuildArticle(ArticleSearchResultItem hit)
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

            return _mvcContext.SitecoreService.GetItem<IArticlePromo>(item.ID.Guid);
        }
    }
}