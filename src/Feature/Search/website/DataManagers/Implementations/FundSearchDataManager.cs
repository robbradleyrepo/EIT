namespace LionTrust.Feature.Search.DataManagers.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Glass.Mapper.Sc;
    using LionTrust.Feature.Search.DataManagers.Interfaces;
    using LionTrust.Feature.Search.Models.API;
    using LionTrust.Feature.Search.Models.API.Facets;
    using LionTrust.Feature.Search.Models.API.Response;
    using LionTrust.Foundation.Content.Repositories;
    using LionTrust.Foundation.Search.Models;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Models.Response;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.Globalization;
    using Sitecore.Links;
    using Sitecore.Resources.Media;

    public class FundSearchDataManager : IFundSearchDataManager
    {
        private readonly IFundContentSearchService _fundContentSearchService;
        private readonly IContentRepository _contentRepository;

        public FundSearchDataManager(IFundContentSearchService fundContentSearchService, IContentRepository contentRepository)
        {
            _fundContentSearchService = fundContentSearchService;
            _contentRepository = contentRepository;
        }

        private string GetArticleDate(DateTime indexedDate)
        {
            var label = string.Empty;
            if(indexedDate.Date == DateTime.Today)
            {
                label = Translate.Text("Today");
            }
            else if (DateTime.Today - indexedDate.Date == TimeSpan.FromDays(1))
            {
                label = Translate.Text("Yesterday");
            }

            if (string.IsNullOrEmpty(label))
            {
                label = indexedDate.ToString("D", Sitecore.Context.Culture);
            }

            return label;
        }

        private IEnumerable<IFundContentResult> MapFundResultHits(IEnumerable<SearchHit<FundSearchResultItem>> hits)
        {
            return hits.Select(x => new FundResult
            {
                FundId = x.Document.ItemId.Guid,
                FundName = x.Document.FundName,
                Url = x.Document.FundPageUrl,
                FundCardImageUrl = x.Document.FundCardImage,
                FundCardHeading = x.Document.FundCardHeading,
                FundCardDescription = x.Document.FundCardDescription,
                CTAText = x.Document.FundPageLinkText,
                FundFactSheet = x.Document.FundFactSheet
            });
        }

        public FundFacetsResponse GetFundFilterFacets(Guid fundFilterFacetConfigId)
        {
            var filterFacetConfigItem = _contentRepository.GetItem<IFundListingFacetsConfig>(new GetItemByIdOptions(fundFilterFacetConfigId));

            var listingFundFacetsResponse = new FundFacetsResponse();
            if(filterFacetConfigItem == null 
                    || filterFacetConfigItem.FundRegionsFolder == null
                    || filterFacetConfigItem.FundManagersFolder == null
                    || filterFacetConfigItem.FundTeamsFolder == null
                    || filterFacetConfigItem.FundRegionsFolder == null)
            {
                return null;
            }

            listingFundFacetsResponse.Facets = new FundFacets
            {
                FundRegions = filterFacetConfigItem.FundRegionsFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString("N"), Name = x.Name }),
                FundManagers = filterFacetConfigItem.FundManagersFolder?.Children?.Where(x => x.IsFundManager)?.Select(x => new FacetItem { Identifier = x.Id.ToString("N"), Name = x.Name }),
                FundTeams = filterFacetConfigItem.FundTeamsFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString("N"), Name = x.Name }),
                FundRanges = filterFacetConfigItem.FundTeamsFolder?.Children?.Select(x => new FacetItem { Identifier = x.Id.ToString("N"), Name = x.Name })
            };

            return listingFundFacetsResponse;
        }

        public ISearchResponse<IFundContentResult> GetFundListingResponse(string database, string fundTeams,string fundManagers, string fundRegions, string fundRanges, string searchTerm, int page)
        {
            page = page - 1;

            var fundSearchRequest = new FundSearchRequest
            {
                DatabaseName = database,
                FundManagers = fundManagers?.Split('|'),
                FundTeams = fundTeams?.Split('|'),
                FundRegions = fundRegions?.Split('|'),
                FundRanges = fundRanges?.Split('|'),
                SearchTerm = searchTerm,
                Skip = page * 21,
                Take = 21,
            };

            var contentSearchResults = this._fundContentSearchService.GetFunds(fundSearchRequest); 
            
            var fundSearchResponse = new SearchResponse<IFundContentResult>();
            if(contentSearchResults.TotalResults > 0)
            {
                fundSearchResponse.SearchResults = this.MapFundResultHits(contentSearchResults.SearchResults);
                fundSearchResponse.StatusMessage = "Success";
                fundSearchResponse.StatusCode = 200;
                fundSearchResponse.TotalResults = contentSearchResults.TotalResults;
            }
            else
            {
                fundSearchResponse.StatusMessage = "No search results found";
                fundSearchResponse.StatusCode = 404;
            }

            return fundSearchResponse;
        }
    }
}