namespace LionTrust.Foundation.Search.Repositories.Interfaces
{
    using System;
    using System.Linq.Expressions;

    using LionTrust.Foundation.Search.Models.ContentSearch;

    public interface IGenericContentSearchRepository
    {
        ContentSearchResults<GenericSearchResultItem> GetGenericSearchResultItems(Expression<Func<GenericSearchResultItem, bool>> predicate, int skip, int take, string database);
    }
}