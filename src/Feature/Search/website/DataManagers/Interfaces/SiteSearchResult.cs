namespace LionTrust.Feature.Search.DataManagers.Interfaces
{
    using LionTrust.Feature.Search.SiteSearch;
    using System.Collections.Generic;

    public class SiteSearchResult
    {
        public int Results { get; set; }

        public IEnumerable<SiteSearchResultItem> Hits { get; set; }
    }
}