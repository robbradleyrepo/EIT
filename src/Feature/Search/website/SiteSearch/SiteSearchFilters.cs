namespace LionTrust.Feature.Search.SiteSearch
{
    using System.Collections.Generic;

    public class SiteSearchFilters
    {
        public string ViewAllText { get; set; }

        public IEnumerable<ISiteSearchFilter> Filters { get; set; }
    }
}
