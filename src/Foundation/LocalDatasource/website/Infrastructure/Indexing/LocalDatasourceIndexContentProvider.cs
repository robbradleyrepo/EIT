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

    public class LocalDatasourceQueryPredicateProvider<T>: ProviderBase, IQueryPredicateProvider<T> where T : SearchResultItem
    {
        public IEnumerable<ID> SupportedTemplates => new[] { TemplateIDs.StandardTemplate };

        public Expression<Func<T, bool>> GetQueryPredicate(IQuery query)
        {
            var fieldNames = new[] { Templates.Index.Fields.LocalDatasourceContent_IndexFieldName };

            return GetFreeTextPredicateService<T>.GetFreeTextPredicate(fieldNames, query);
        }
    }
}