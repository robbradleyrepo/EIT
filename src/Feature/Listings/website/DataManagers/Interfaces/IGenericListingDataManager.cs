namespace LionTrust.Feature.Listings.DataManagers.Interfaces
{
    using System;
    using System.Collections.Generic;
    using LionTrust.Feature.Listings.Models.API.Response;
    using LionTrust.Foundation.Search.Models.Response;

    public interface IGenericListingDataManager
    {
        GenericListerFacetResponse GetGenericListingFilterFacets(Guid articleFilterFacetConfigId);
        IGenericSearchResponse GetGenericListingResponse(string parent, string type = "", List<int> months = null, List<int> years = null, string searchTerm = "", int page = 1, string database = "web");
    }
}