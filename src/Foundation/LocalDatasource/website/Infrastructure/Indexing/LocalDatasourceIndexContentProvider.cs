namespace LionTrust.Foundation.LocalDatasource.Infrastructure.Indexing
{
    using System;
    using System.Collections.Generic;
    using System.Configuration.Provider;
    using System.Linq.Expressions;

    using LionTrust.Foundation.Indexing.Services;
    using LionTrust.Foundation.Indexing.Models;
    using Sitecore;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Data;
    using Sitecore.ContentSearch.Linq.Utilities;

    public class LocalDatasourceQueryPredicateProvider<T>: ProviderBase, IQueryPredicateProvider<T> where T : SearchResultItem
    {
        public IEnumerable<ID> SupportedTemplates => new[] { TemplateIDs.StandardTemplate };

        public Expression<Func<T, bool>> GetQueryPredicate(IQuery query)
        {
            var fields = new[] { new Field { FieldName = Templates.Index.Fields.LocalDatasourceContent_IndexFieldName, Boost = 9f }};

            var predicate = GetTextPredicateService<T>.GetFreeTextPredicate(fields, query);

            if (query.SimilarResults)
            {
                var expression = GetTextPredicateService<T>.GetFuzzyTextPredicate(fields, query);
                predicate = predicate.Or(expression);
            }

            return predicate;
        }
    }
}