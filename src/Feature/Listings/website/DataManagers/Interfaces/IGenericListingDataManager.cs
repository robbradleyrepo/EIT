namespace LionTrust.Feature.Listings.DataManagers.Interfaces
{
    using System;
    using LionTrust.Feature.Listings.Models.API.Response;
    using LionTrust.Foundation.Search.Models.Response;

    public interface IGenericListingDataManager
    {
        GenericListerFacetResponse GetGenericListingFilterFacets(Guid articleFilterFacetConfigId);
        IGenericSearchResponse GetGenericListingResponse(string database, string parentId, string listingType, int? month, int? year, string searchTerm, int page);
    }
}