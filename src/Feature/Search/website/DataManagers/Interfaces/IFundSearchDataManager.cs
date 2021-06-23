namespace LionTrust.Feature.Search.DataManagers.Interfaces
{
    using System;

    using LionTrust.Feature.Search.Models.API.Response;
    using LionTrust.Foundation.Search.Models;
    using LionTrust.Foundation.Search.Models.Response;

    public interface IFundSearchDataManager
    {
        FundFacetsResponse GetFundFilterFacets(Guid fundFilterFacetConfigId);

        ISearchResponse<IFundContentResult> GetFundListingResponse(string database, string fundTeams, string fundCategories, string fundManagers, string searchTerm, int page);
    }
}