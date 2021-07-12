namespace LionTrust.Foundation.Search.Repositories.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using LionTrust.Foundation.Search.Models.ContentSearch;

    public interface IGenericContentSearchRepository
    {
        GenericSearchResults GetGenericSearchResultItems(Expression<Func<GenericSearchResultItem, bool>> predicate, int skip, int take, string database);
    }
}