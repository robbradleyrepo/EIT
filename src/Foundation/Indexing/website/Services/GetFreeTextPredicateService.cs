namespace LionTrust.Foundation.Indexing.Services
{
    using System;
    using System.Linq.Expressions;

    using LionTrust.Foundation.Indexing.Models;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.ContentSearch.SearchTypes;

    public static class GetFreeTextPredicateService<T> where T : SearchResultItem
    {
        public static Expression<Func<T, bool>> GetFreeTextPredicate(string[] fieldNames, float[] boosting, IQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.QueryText))
            {
                return PredicateBuilder.True<T>();
            }

            var predicate = PredicateBuilder.False<T>();
            var counter = 0;
            foreach (var name in fieldNames)
            {
                var boostValue = boosting[counter];
                predicate = predicate.Or(i => i[name].Contains(query.QueryText).Boost(boostValue));
                counter++;
            }

            return predicate;
        }
    }
}