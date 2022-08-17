namespace LionTrust.Feature.Search.SiteSearch
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

    public class LegacyQueryPredicateProvider<T> : ProviderBase, IQueryPredicateProvider<T> where T : SearchResultItem
    {
        public IEnumerable<ID> SupportedTemplates => new[] { TemplateIDs.StandardTemplate };

        public Expression<Func<T, bool>> GetQueryPredicate(IQuery query)
        {
            var fields = new[] { 
                new Field{ FieldName = "legacypresentationbase_pagetitle", Boost = 10f },
                new Field{ FieldName = "legacy_content", Boost = 9f },
                new Field{ FieldName = "LegacyDocument_DocumentName", Boost = 10f },
                new Field{ FieldName = "related_fund_name", Boost = 2f }};

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