namespace LionTrust.Foundation.Search.Models.ContentSearch
{
    using System.Collections.Generic;

    using Sitecore.ContentSearch.Linq;

    public class ContentSearchResults
    {
        public IEnumerable<SearchHit<ArticleSearchResultItem>> SearchResults { get; set; }
        public int TotalResults { get; set; }
    }
}