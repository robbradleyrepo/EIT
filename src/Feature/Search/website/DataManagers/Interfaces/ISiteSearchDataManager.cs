namespace LionTrust.Feature.Search.DataManagers.Interfaces
{
    using LionTrust.Feature.Search.Models.API.Response;
    using LionTrust.Feature.Search.SiteSearch;
    using LionTrust.Foundation.Search.Models.Response;
    using System;

    public interface ISiteSearchDataManager
    {
        ISearchResponse<SiteSearchHit> Search(string query, string database, string templatesIds, string language, int resultsPerPage, int page);
        FacetsResponse GetFilterFacets(Guid facetConfigId);
    }
}