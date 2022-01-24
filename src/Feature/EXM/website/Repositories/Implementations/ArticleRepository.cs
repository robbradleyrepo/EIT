namespace LionTrust.Feature.EXM.Repositories.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.EXM.Repositories.Interfaces;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Services.Interfaces;

    public class ArticleRepository : IArticleRepository
    {
        private readonly IArticleContentSearchService _searchService;
        private readonly IMvcContext _mvcContext;

        public ArticleRepository(IArticleContentSearchService searchService, IMvcContext mvcContext)
        {
            this._searchService = searchService;
            this._mvcContext = mvcContext;
        }

        public IEnumerable<ArticleSearchResultItem> GetLatestArticles(int limit)
        {
            var request = new ArticleSearchRequest
            {
                FromDate = DateTime.MinValue,
                ToDate = DateTime.MaxValue,
                Take = limit,
                DatabaseName = _mvcContext.SitecoreService.Database.Name,
            };

            var results = _searchService.GetDatedTaxonomyRelatedArticles(request, result => result.OrderByDescending(hit => hit.Created));
            if (results == null || results.SearchResults == null)
            {
                return null;
            }

            var articlePromos = results.SearchResults
                .Where(sr => sr.Document != null)
                .Select(sr => sr.Document);

            return articlePromos;
        }
    }
}