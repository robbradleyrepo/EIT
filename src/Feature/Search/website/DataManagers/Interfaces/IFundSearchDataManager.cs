namespace LionTrust.Feature.Search.DataManagers.Interfaces
{
    using System;

    using LionTrust.Feature.Search.Models.API.Response;
    using LionTrust.Foundation.Search.Models;
    using LionTrust.Foundation.Search.Models.Response;

    public interface IFundSearchDataManager
    {
        FacetsResponse GetFundFilterFacets(Guid fundFilterFacetConfigId);

        ISearchResponse<IFundContentResult> GetFundListingResponse(string database, string fundTeams, string fundManagers, string fundRegions, string fundRanges, string searchTerm, int page);
    }
}