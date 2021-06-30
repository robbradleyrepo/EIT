namespace LionTrust.Feature.Listings.DataManagers.Interfaces
{
    using System;
    using LionTrust.Feature.Listings.Models;
    using LionTrust.Foundation.Search.Models.Response;

    public interface IGenericListingDataManager
    {
        GenericListerFacetResponse GetGenericListingFilterFacets(Guid articleFilterFacetConfigId);
    }
}