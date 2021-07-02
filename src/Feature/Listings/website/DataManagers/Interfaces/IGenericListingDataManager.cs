namespace LionTrust.Feature.Listings.DataManagers.Interfaces
{
    using System;
    using System.Collections.Generic;
    using LionTrust.Feature.Listings.Models.API.Response;
    using LionTrust.Foundation.Search.Models.Response;

    public interface IGenericListingDataManager
    {
        GenericListerFacetResponse GetGenericListingFilterFacets(Guid articleFilterFacetConfigId);
        IGenericSearchResponse GetGenericListingResponse(string database, string parentId, string listingType, List<int> months, List<int> years, string searchTerm, int page);
    }
}