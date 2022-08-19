namespace LionTrust.Foundation.Indexing.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using LionTrust.Foundation.Indexing.Models;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.ContentSearch.SearchTypes;

    public static class GetTextPredicateService<T> where T : SearchResultItem
    {
        public static Expression<Func<T, bool>> GetFreeTextPredicate(Field[] fields, IQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.QueryText))
            {
                return PredicateBuilder.True<T>();
            }

            var terms = query.QueryText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            terms = terms.Except(Lucene.Net.Analysis.Standard.StandardAnalyzer.STOP_WORDS_SET, StringComparer.OrdinalIgnoreCase).ToList();

            var predicate = PredicateBuilder.False<T>();
            foreach (var field in fields)
            {
                predicate = predicate.Or(i => i[field.FieldName].Contains(query.QueryText).Boost(field.Boost + 12f));

                var expression = PredicateBuilder.True<T>();
                expression = terms.Aggregate(expression, (current, term) => current.And(i => i[field.FieldName].Contains(term).Boost(field.Boost)));
                predicate = predicate.Or(expression);
            }

            return predicate;
        }

        public static Expression<Func<T, bool>> GetFuzzyTextPredicate(Field[] fields, IQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.QueryText))
            {
                return PredicateBuilder.True<T>();
            }

            var terms = query.QueryText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            terms = terms.Except(Lucene.Net.Analysis.Standard.StandardAnalyzer.STOP_WORDS_SET, StringComparer.OrdinalIgnoreCase).ToList();

            var predicate = PredicateBuilder.False<T>();
            foreach (var field in fields)
            {
                predicate = predicate.Or(i => i[field.FieldName].Contains(query.QueryText).Boost(field.Boost + 12f)
                                                || i[field.FieldName].Like(query.QueryText, 0.8f).Boost(field.Boost + 4f));

                var expression = PredicateBuilder.True<T>();
                expression = terms.Aggregate(expression, (current, term) => 
                                current.And(i => i[field.FieldName].Contains(term).Boost(field.Boost + 1f) || i[field.FieldName].Like(term, 0.8f).Boost(field.Boost)));
                predicate = predicate.Or(expression);
            }

            return predicate;
        }
    }
}