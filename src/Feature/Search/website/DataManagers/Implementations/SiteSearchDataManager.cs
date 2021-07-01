namespace LionTrust.Feature.Search.DataManagers.Implementations
{
    using System.Linq;
    using System.Collections.Generic;
    using LionTrust.Feature.Search.DataManagers.Interfaces;
    using LionTrust.Foundation.Indexing.Models;
    using LionTrust.Foundation.LocalDatasource.Infrastructure.Indexing;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Security;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.Data;
    using LionTrust.Feature.Search.SiteSearch;

    public class SiteSearchDataManager : ISiteSearchDataManager
    {
        public class BasicQuery : IQuery
        {
            public string QueryText { get; set; }

            public int NoOfResults { get; set; }

            public Dictionary<string, string[]> Facets { get; set; }

            public int Page { get; set; }
        }

        public SiteSearchResult Search(string query, string database, string[] templatesIds, string language, int resultsPerPage, int page)
        {
            var startPage = resultsPerPage * (page - 1);
            using (IProviderSearchContext context = ContentSearchManager
                                                            .GetIndex($"liontrust_search_{database}_index")
                                                            .CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
            {
                
                var predicate = new LocalDatasourceQueryPredicateProvider<SiteSearchResultItem>().GetQueryPredicate(new BasicQuery { QueryText = query });
                var legacyPredicate = new LegacyQueryPredicateProvider<SiteSearchResultItem>().GetQueryPredicate(new BasicQuery { QueryText = query });

                predicate = predicate.Or(legacyPredicate);
                if (templatesIds != null && templatesIds.Length > 0)
                {
                    var templateQuery = PredicateBuilder.True<SiteSearchResultItem>();
                    foreach (var item in templatesIds)
                    {
                        templateQuery = templateQuery.Or(s => s.TemplateId == new ID(item));
                    }

                    predicate = predicate.And(templateQuery);
                }
                var searchQuery = context.GetQueryable<SiteSearchResultItem>()
                    .Where(predicate)
                    .Where(r => r.Language == language)
                    .Skip(startPage)
                    .Take(resultsPerPage);
                    
                var results = searchQuery.GetResults();
                return new SiteSearchResult { Results = results.TotalSearchResults, Hits = results.Hits.Select(h => h.Document) };
            }
        }

        public SiteSearchResult Search(string query, string database, string language, int resultsPerPage, int page)
        {
            return Search(query, database, new string[0], language, resultsPerPage, page);
        }
    }
}