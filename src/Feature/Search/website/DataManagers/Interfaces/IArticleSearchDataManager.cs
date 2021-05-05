namespace LionTrust.Feature.Search.DataManagers.Interfaces
{
    using System;

    using LionTrust.Feature.Search.Models.API.Response;
    using LionTrust.Foundation.Search.Models.Response;

    public interface IArticleSearchDataManager
    {
        ArticleFacetsResponse GetArticleFilterFacets(Guid articleFilterFacetConfigId);

        ITaxonomySearchResponse GetArticleListingResponse(string database, string funds, string fundCategories, string fundManagers, string fundTeams, int? month, int? year, string searchTerm, int page);
    }
}