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
            var filterFacetConfigItem = _contentRepository.GetItem<IGenericListingFilters>(new GetItemByIdOptions(genericFilterFacetConfigId));

            var listingGenericListingResponse = new GenericListerFacetResponse();
            listingGenericListingResponse.Facets = new GenericListingFacets();
            if (filterFacetConfigItem != null)
            {
                if (filterFacetConfigItem.ListingTypeList != null)
                {
                    listingGenericListingResponse.Facets.ListingItemTypes = filterFacetConfigItem.ListingTypeList.Select(x => new ListingFilterFacetsModel { Name = x.ListingItemTypeName, Identifier = x.Id.ToString() });
                }

                if (filterFacetConfigItem.Months != null)
                {
                    listingGenericListingResponse.Facets.Months = filterFacetConfigItem.Months.Select(x=> new ListingFilterFacetsModel { Name = x.Title, Identifier = x.Value });
                }

                if (filterFacetConfigItem.Years != null)
                {
                    listingGenericListingResponse.Facets.Years = filterFacetConfigItem.Years.Select(x => new ListingFilterFacetsModel { Name = x.Title, Identifier = !string.IsNullOrEmpty(x.Value) ? x.Value : x.Name });
                }
            }

            return listingGenericListingResponse;
        }

        public IGenericSearchResponse GetGenericListingResponse(string database, string parent, string listingType, List<int> months, List<int> years, string searchTerm, int page)
        {
            page = page - 1;

            var genericSearchRequest = new GenericSearchRequest
            {
                DatabaseName = database,
                Parent = parent,
                ListingType = listingType?.Split('|'),
                Months = months,
                Years = years,
                SearchTerm = searchTerm,
                Skip = page * 21,
                Take = 21
            };

            GenericSearchResults contentSearchResults;
            contentSearchResults = this._genericContentSearchService.GetTaxonomyRelatedGenericItems(genericSearchRequest);
            
            var genericSearchResponse = new GenericListingSearchResponse();
            if (contentSearchResults.TotalResults > 0)
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