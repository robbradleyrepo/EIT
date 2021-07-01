namespace LionTrust.Foundation.Search.Services.Interfaces
{
    using System;
    using System.Linq;

    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;

    public interface IArticleContentSearchService
    {
        ContentSearchResults<ArticleSearchResultItem> GetDatedTaxonomyRelatedArticles(ITaxonomySearchRequest articleSearchRequest, Func<IQueryable<ArticleSearchResultItem>, IQueryable<ArticleSearchResultItem>> sort = null);
    }
}