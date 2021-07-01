namespace LionTrust.Foundation.Search.Services.Interfaces
{
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using System;
    using System.Linq;

    public interface ISiteContentSearchService
    {
        ContentSearchResults GetDatedTaxonomyRelatedArticles(ITaxonomySearchRequest articleSearchRequest, Func<IQueryable<ArticleSearchResultItem>, IQueryable<ArticleSearchResultItem>> sort = null);
    }
}