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

    public class LegacyQueryPredicateProvider<T> : ProviderBase, IQueryPredicateProvider<T> where T : SearchResultItem
    {
        public IEnumerable<ID> SupportedTemplates => new[] { TemplateIDs.StandardTemplate };

        public Expression<Func<T, bool>> GetQueryPredicate(IQuery query)
        {
            var fieldNames = new[] { "legacy_content", "LegacyDocument_DocumentName", "related_fund_name" };

            return GetFreeTextPredicateService<T>.GetFreeTextPredicate(fieldNames, query);
        }
    }
}