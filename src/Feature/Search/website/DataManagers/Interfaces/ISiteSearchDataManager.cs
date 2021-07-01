namespace LionTrust.Feature.Search.DataManagers.Interfaces
{
    using System.Collections.Generic;

    public interface ISiteSearchDataManager
    {
        SiteSearchResult Search(string query, string database, string[] templatesIds, string language, int resultsPerPage, int page);

        SiteSearchResult Search(string query, string database, string language, int resultsPerPage, int page);
    }
}