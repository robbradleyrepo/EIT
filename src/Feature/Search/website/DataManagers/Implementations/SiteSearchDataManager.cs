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
    using LionTrust.Foundation.Search.Models.Response;
    using LionTrust.Feature.Search.Models.API.Response;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using SiteSearchResultItem = SiteSearch.SiteSearchResultItem;
    using System;
    using LionTrust.Foundation.Content.Repositories;
    using Glass.Mapper.Sc;
    using LionTrust.Feature.Search.Models.API;
    using Log = Sitecore.Diagnostics.Log;
    using LionTrust.Foundation.Onboarding.Helpers;

    public class SiteSearchDataManager : ISiteSearchDataManager
    {
        private readonly IContentRepository _contentRepository;

        public SiteSearchDataManager(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }
        public class BasicQuery : IQuery
        {
            public string QueryText { get; set; }

            public int NoOfResults { get; set; }

            public Dictionary<string, string[]> Facets { get; set; }

            public int Page { get; set; }
        }

        public ISearchResponse<SiteSearchHit> Search(string query, string database, string templatesIds, string language, int resultsPerPage, int page)
        {
            var SearchResponse = new SearchResponse<SiteSearchHit>();
            try
            {
                var startPage = resultsPerPage * (page - 1);
                using (IProviderSearchContext context = ContentSearchManager
                                                                .GetIndex($"liontrust_search_{database}_index")
                                                                .CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
                {

                    var predicate = new LocalDatasourceQueryPredicateProvider<SiteSearchResultItem>().GetQueryPredicate(new BasicQuery { QueryText = query });
                    var legacyPredicate = new LegacyQueryPredicateProvider<SiteSearchResultItem>().GetQueryPredicate(new BasicQuery { QueryText = query });

                    predicate = predicate.Or(legacyPredicate);
                    if (!string.IsNullOrWhiteSpace(templatesIds))
                    {
                        var templateQuery = PredicateBuilder.True<SiteSearchResultItem>();
                        foreach (var id in templatesIds.Split('|'))
                        {
                            var templateId = new ID(id);

                            if (!ID.IsNullOrEmpty(templateId))
                            {
                                templateQuery = templateQuery.Or(s => s.TemplateId == templateId);
                            }
                        }

                        predicate = predicate.And(templateQuery);
                        
                    }

                    var country = OnboardingHelper.GetCurrentContactCountryCode();
                    predicate = predicate.And(x => !x.ExcludedCountries.Contains(country));

                    predicate = predicate.And(x => x.IncludeInSearchResults);

                    var searchQuery = context.GetQueryable<SiteSearchResultItem>()
                        .Where(predicate)
                        .Where(r => r.Language == language)
                        .OrderBy(r => r.Priority)
                        .ThenByDescending(r => r["score"])
                        .ThenByDescending(r => r.ArticleCreatedDate)
                        .Skip(startPage)
                        .Take(resultsPerPage);

                    var results = searchQuery.GetResults();

                    if (results == null)
                    {
                        return null;
                    }

                    var contentSearchResults = new ContentSearchResults<SiteSearchResultItem> { SearchResults = results, TotalResults = results.TotalSearchResults };


                    if (contentSearchResults.TotalResults > 0)
                    {
                        SearchResponse.SearchResults = MapFundResultHits(contentSearchResults.SearchResults);
                        SearchResponse.StatusMessage = "Success";
                        SearchResponse.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        SearchResponse.TotalResults = contentSearchResults.TotalResults;
                    }
                    else
                    {
                        SearchResponse.StatusMessage = "No search results found";
                        SearchResponse.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
                    }
                }
            }
            catch (Exception ex)
            {
                SearchResponse.StatusMessage = "An error occured when trying search";
                SearchResponse.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                Log.Error(SearchResponse.StatusMessage, ex, this);
            }

            return SearchResponse;

        }

        private IEnumerable<SiteSearchHit> MapFundResultHits(IEnumerable<SearchHit<SiteSearchResultItem>> hits)
        {
            var results = new List<SiteSearchHit>();
            foreach (var hit in hits)
            {
                if (hit.Document != null)
                {
                    var relatedFundName = !string.IsNullOrEmpty(hit.Document.RelatedFundName)
                                          ? hit.Document.RelatedFundName.Split('|')[0]
                                          : string.Empty;
                    var siteSearchHit = new SiteSearchHit
                    {
                        Url = hit.Document.PageUrl,
                        Copy = !string.IsNullOrWhiteSpace(hit.Document.DocumentDescription) ? hit.Document.DocumentDescription : hit.Document.Copy,
                        PageTitle = !string.IsNullOrWhiteSpace(hit.Document.DocumentName) ? hit.Document.DocumentName : hit.Document.PageTitle,
                        Author = hit.Document.Authors,
                        AuthorImage = hit.Document.AuthorImageUrl,
                        FundTeam = hit.Document.FundTeamName,
                        FundTeamUrl = hit.Document.FundTeamPage,
                        ResultType = hit.Document.ResultType,
                        PageDate = hit.Document.ArticleCreatedDate.ToString("dd MMMM yyyy"),
                        TemplateId = hit.Document.TemplateId.Guid,
                        FactsheetUrl = hit.Document.FactSheetUrl,
                        RelatedFundName = relatedFundName,
                        RelatedFundUrl = hit.Document.RelatedFundUrl
                    };

                    results.Add(siteSearchHit);
                }
            }

            return results;
        }

        public FacetsResponse GetFilterFacets(Guid facetConfigId)
        {
            var facetConfigItem = _contentRepository.GetItem<ISiteSearch>(new GetItemByIdOptions(facetConfigId));

            var facetResponse = new FacetsResponse();
            if (facetConfigItem == null || facetConfigItem.Filters == null || !facetConfigItem.Filters.Any())
            {
                return null;
            }

            var facets = new List<Facet>();


            foreach (var filter in facetConfigItem.Filters)
            {
                facets.Add(new Facet
                {
                    Name = filter.FilterName,
                    Items = filter.PageTemplates.Select(t => new FacetItem { Identifier = t.Id.ToString("N"), Name = t.Name })
                });
            }

            facetResponse.Facets = facets;

            return facetResponse;
        }
    }
}