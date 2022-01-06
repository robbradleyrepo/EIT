namespace LionTrust.Feature.Listings.DataManagers.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Glass.Mapper.Sc;
    using LionTrust.Feature.Listings.DataManagers.Interfaces;
    using LionTrust.Feature.Listings.Models;
    using LionTrust.Feature.Listings.Models.API.Facets;
    using LionTrust.Feature.Listings.Models.API.Response;
    using LionTrust.Foundation.Content.Repositories;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.Search.Models;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Models.Response;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.ContentSearch.Linq;

    public class GenericListingDataManager : IGenericListingDataManager
    {
        private readonly IGenericContentSearchService _genericContentSearchService;
        private readonly IContentRepository _contentRepository;

        public GenericListingDataManager(IGenericContentSearchService genericContentSearchService, IContentRepository contentRepository)
        {
            _genericContentSearchService = genericContentSearchService;
            _contentRepository = contentRepository;
        }

        public GenericListerFacetResponse GetGenericListingFilterFacets(Guid genericFilterFacetConfigId)
        {
            var filterFacetConfigItem = _contentRepository.GetItem<IGenericListingFacets>(new GetItemByIdOptions(genericFilterFacetConfigId));

            var listingGenericListingResponse = new GenericListerFacetResponse();
            listingGenericListingResponse.Facets = new List<GenericListingFacet>();
            if (filterFacetConfigItem != null)
            {                
                if (filterFacetConfigItem.Months != null && filterFacetConfigItem.Months.Any())
                {
                    listingGenericListingResponse.Facets.Add(new GenericListingFacet { Name = "Month", Items = filterFacetConfigItem.Months.Select(x => new ListingFilterFacetsModel { Name = x.Title, Identifier = x.Value }) });
                }

                if (filterFacetConfigItem.Years != null && filterFacetConfigItem.Years.Any())
                {
                    listingGenericListingResponse.Facets.Add(new GenericListingFacet { Name = "Year", Items = filterFacetConfigItem.Years.Select(x => new ListingFilterFacetsModel { Name = x.Title, Identifier = !string.IsNullOrEmpty(x.Value) ? x.Value : x.Name }) });
                }

                if (filterFacetConfigItem.ListingTypeList != null && filterFacetConfigItem.ListingTypeList.Any())
                {
                    listingGenericListingResponse.Facets.Add(new GenericListingFacet { Name = "ListingType", Items = filterFacetConfigItem.ListingTypeList.Select(x => new ListingFilterFacetsModel { Name = x.ListingItemTypeName, Identifier = x.Id.ToString() }) });
                }
            }

            return listingGenericListingResponse;
        }

        public IGenericSearchResponse GetGenericListingResponse(string parent, string listingType = "", List<int> months = null, List<int> years = null, string searchTerm = "", int page = 1, string database = "web")
        {
            page = page - 1;
            var pageSize = Constants.Pagination.PageSize;

            var genericSearchRequest = new GenericSearchRequest
            {
                DatabaseName = database,
                Parent = parent,                
                Months = months,
                Years = years,
                SearchTerm = searchTerm,
                Skip = page * pageSize,
                Take = pageSize
            };

            if (!string.IsNullOrEmpty(listingType))
            {
                genericSearchRequest.ListingType = listingType.Split('|');
            }

            ContentSearchResults<GenericSearchResultItem> contentSearchResults;
            contentSearchResults = this._genericContentSearchService.GetTaxonomyRelatedGenericItems(genericSearchRequest);
            
            var genericSearchResponse = new GenericListingSearchResponse();
            if (contentSearchResults != null && contentSearchResults.TotalResults > 0)
            {
                genericSearchResponse.SearchResults = this.MapGenericResultHits(contentSearchResults.SearchResults);
                genericSearchResponse.StatusMessage = "Success";
                genericSearchResponse.StatusCode = 200;
                genericSearchResponse.TotalResults = contentSearchResults.TotalResults;
            }
            else
            {
                genericSearchResponse.StatusMessage = "No search results found";
                genericSearchResponse.StatusCode = 404;
            }

            return genericSearchResponse;
        }

        private IEnumerable<IGenericContentResult> MapGenericResultHits(IEnumerable<SearchHit<GenericSearchResultItem>> hits)
        {
            return hits.Select(x => new GenericListingResult
            {
                ImageWidth = x.Document.GenericListingImageWidth,
                ImageUrl = x.Document.GenericListingImage,
                Subtitle = x.Document.GenericListingSubtitle,
                Title = x.Document.GenericListingTitle,
                Date = x.Document.Created.ToString("dd.MM.yyyy"),
                Text = x.Document.GenericListingText,
                ListingType = x.Document.GenericListingType
            });
        }
    }
}