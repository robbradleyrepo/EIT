namespace LionTrust.Feature.Search.SiteSearch
{
    using System.Collections.Generic;

    public class SiteSearchViewModel
    {
        public ISiteSearch Configuration { get; set; }

        public IEnumerable<SiteSearchHit> Results { get; set; }

        public string Query { get; set; }

        public int TotalResults { get; set; }
    }
}