namespace LionTrust.Foundation.Indexing.Services
{
    using System;
    using System.Linq.Expressions;

    using LionTrust.Foundation.Indexing.Models;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.ContentSearch.SearchTypes;

    public static class GetFreeTextPredicateService<T> where T : SearchResultItem
    {
        public static Expression<Func<T, bool>> GetFreeTextPredicate(string[] fieldNames, IQuery query)
        {
            var predicate = PredicateBuilder.False<T>();
            if (string.IsNullOrWhiteSpace(query.QueryText))
            {
                return predicate;
            }

            foreach (var name in fieldNames)
            {
                predicate = predicate.Or(i => i[name].Contains(query.QueryText));
            }

            return predicate;
        }
    }
}