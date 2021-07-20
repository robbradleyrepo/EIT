﻿namespace LionTrust.Feature.Search.DataManagers.Implementations
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
    using LionTrust.Foundation.Search.Models.Response;
    using LionTrust.Feature.Search.Models.API.Response;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using SiteSearchResultItem = SiteSearch.SiteSearchResultItem;
    using System;

    public class SiteSearchDataManager : ISiteSearchDataManager
    {
        public class BasicQuery : IQuery
        {
            public string QueryText { get; set; }

            public int NoOfResults { get; set; }

            public Dictionary<string, string[]> Facets { get; set; }

            public int Page { get; set; }
        }

        public ISearchResponse<SiteSearchHit> Search(string query, string database, string[] templatesIds, string language, int resultsPerPage, int page)
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

                if (results == null)
                {
                    return null;
                }

                var contentSearchResults = new ContentSearchResults<SiteSearchResultItem> { SearchResults = results, TotalResults = results.TotalSearchResults };

                var SearchResponse = new SearchResponse<SiteSearchHit>();
                if (contentSearchResults.TotalResults > 0)
                {
                    SearchResponse.SearchResults = MapFundResultHits(contentSearchResults.SearchResults);
                    SearchResponse.StatusMessage = "Success";
                    SearchResponse.StatusCode = 200;
                    SearchResponse.TotalResults = contentSearchResults.TotalResults;
                }
                else
                {
                    SearchResponse.StatusMessage = "No search results found";
                    SearchResponse.StatusCode = 404;
                }

                return SearchResponse;
            }
        }

        private IEnumerable<SiteSearchHit> MapFundResultHits(IEnumerable<SearchHit<SiteSearchResultItem>> hits)
        {
            var results = new List<SiteSearchHit>();
            foreach (var hit in hits)
            {
                if (hit.Document != null)
                {           
                    var siteSearchHit = new SiteSearchHit
                    {
                        Url = hit.Document.PageUrl,
                        Copy = hit.Document.Copy,
                        PageTitle = hit.Document.PageTitle,
                        Author = hit.Document.Authors,
                        AuthorImage = hit.Document.AuthorImageUrl,
                        FundTeam = hit.Document.FundTeamName,
                        FundTeamUrl = hit.Document.FundTeamPage,
                        ResultType = hit.Document.ResultType,
                        PageDate = hit.Document.Updated.ToString("dd MMMM yyyy"),
                        TemplateId = hit.Document.TemplateId.Guid,
                        FactsheetUrl = hit.Document.FactSheetUrl
                    };

                    results.Add(siteSearchHit);
                }
            }

            return results;
        }
    }
}