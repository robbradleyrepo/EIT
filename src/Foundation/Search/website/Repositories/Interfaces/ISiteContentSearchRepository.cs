namespace LionTrust.Foundation.Search.Repositories.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using LionTrust.Foundation.Search.Models.ContentSearch;

    public interface ISiteContentSearchRepository
    {
        ContentSearchResults GetArticleSearchResultItems(Expression<Func<ArticleSearchResultItem, bool>> predicate, int skip, int take, string database, Func<IQueryable<ArticleSearchResultItem>, IQueryable<ArticleSearchResultItem>> sort);
    }
}