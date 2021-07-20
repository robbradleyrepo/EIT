namespace LionTrust.Feature.Search.DataManagers.Interfaces
{
    using LionTrust.Feature.Search.SiteSearch;
    using LionTrust.Foundation.Search.Models.Response;

    public interface ISiteSearchDataManager
    {
        ISearchResponse<SiteSearchHit> Search(string query, string database, string[] templatesIds, string language, int resultsPerPage, int page);
    }
}