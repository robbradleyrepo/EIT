namespace LionTrust.Foundation.Search.Models.ContentSearch
{
    using System.Collections.Generic;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.SearchTypes;

    public class ContentSearchResults <T> where T : BaseSearchResultItem
    {
        public IEnumerable<SearchHit<T>> SearchResults { get; set; }
        public int TotalResults { get; set; }
    }
}