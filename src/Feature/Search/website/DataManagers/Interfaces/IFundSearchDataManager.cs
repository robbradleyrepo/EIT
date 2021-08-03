namespace LionTrust.Feature.Search.DataManagers.Interfaces
{
    using System;
    using System.Collections.Generic;
    using LionTrust.Feature.Search.Models.API.Response;
    using LionTrust.Foundation.Search.Models;
    using LionTrust.Foundation.Search.Models.Response;
    using Sitecore.Data;

    public interface IFundSearchDataManager
    {
        FacetsResponse GetFundFilterFacets(Guid fundFilterFacetConfigId);

        ISearchResponse<IFundContentResult> GetFundListingResponse(string database, string fundTeams, string fundManagers, string fundRegions, string fundRanges, string searchTerm, string sortOrder, int page);

        ISearchResponse<IFundContentResult> GetMyFundListingResponse(string database, string fundTeams, IEnumerable<string> citiCodes, IEnumerable<string> excludecitiCodes, string sortOrder, int page);

    }
}