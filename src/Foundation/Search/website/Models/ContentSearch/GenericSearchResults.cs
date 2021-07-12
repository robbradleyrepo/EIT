namespace LionTrust.Foundation.Search.Models.ContentSearch
{
    using Sitecore.ContentSearch.Linq;
    using System.Collections.Generic;

    public class GenericSearchResults
    {
        public IEnumerable<SearchHit<GenericSearchResultItem>> SearchResults { get; set; }
        public int TotalResults { get; set; }
    }
}