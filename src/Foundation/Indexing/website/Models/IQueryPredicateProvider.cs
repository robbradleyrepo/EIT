namespace LionTrust.Foundation.Indexing.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Data;

    public interface IQueryPredicateProvider<T> where T : SearchResultItem
    {
        Expression<Func<T, bool>> GetQueryPredicate(IQuery query);
        IEnumerable<ID> SupportedTemplates { get; }
    }
}