namespace LionTrust.Feature.Listings.DataManagers.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Glass.Mapper.Sc;
    using LionTrust.Feature.Listings.DataManagers.Interfaces;
    using LionTrust.Feature.Listings.Models;
    using LionTrust.Foundation.Content.Repositories;
    using LionTrust.Foundation.Search.Models;
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using LionTrust.Foundation.Search.Models.Response;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.Globalization;
    using Sitecore.Resources.Media;

    public class GenericListingDataManager : IGenericListingDataManager
    {
        private readonly IContentRepository _contentRepository;

        public GenericListingDataManager(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public GenericListerFacetResponse GetGenericListingFilterFacets(Guid articleFilterFacetConfigId)
        {
            var filterFacetConfigItem = _contentRepository.GetItem<IGenericListingFacetsConfig>(new GetItemByIdOptions(articleFilterFacetConfigId));

            var listingGenericListingResponse = new GenericListerFacetResponse();
            if (filterFacetConfigItem == null 
                    || filterFacetConfigItem.ListingItemTypeFolder == null)
            {
                return null;
            }

            listingGenericListingResponse.Facets = new GenericListingFacets {
                                                    ListingItemTypes = filterFacetConfigItem.ListingItemTypeFolder.Children
            };

            return listingGenericListingResponse;
        }

        public GenericListingResponse GetGenericListingResponse()
        {

        }
    }
}