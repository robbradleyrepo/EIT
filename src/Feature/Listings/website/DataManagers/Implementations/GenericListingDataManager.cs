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
            var filterFacetConfigItem = _contentRepository.GetItem<IGenericListingFacetsConfig>(new GetItemByIdOptions(genericFilterFacetConfigId));

            var listingGenericListingResponse = new GenericListerFacetResponse();
            if (filterFacetConfigItem == null 
                    || filterFacetConfigItem.ListingTypeList == null)
            {
                return null;
            }
            listingGenericListingResponse.Facets = new GenericListingFacets {
                ListingItemTypes = filterFacetConfigItem.ListingTypeList.Select(x => new ListingItemTypeModel { Name = x.ListingItemTypeName, Identifier = x.Id.ToString() })
            };

            return listingGenericListingResponse;
        }

        public IGenericSearchResponse GetGenericListingResponse(string database, string parent, string listingType, int? month, int? year, string searchTerm, int page)
        {
            var fromYear = year ?? 2000;
            var fromMonth = month ?? 1;
            var toYear = year ?? DateTime.Now.Year + 1;
            var toMonth = month ?? 12;
            page = page - 1;

            var genericSearchRequest = new GenericSearchRequest
            {
                DatabaseName = database,
                FromDate = new DateTime(fromYear, fromMonth, 1),
                Parent = parent,
                ListingType = listingType?.Split('|'),
                SearchTerm = searchTerm,
                Skip = page * 21,
                Take = 21,
                ToDate = new DateTime(toYear, toMonth, DateTime.DaysInMonth(toYear, toMonth))
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