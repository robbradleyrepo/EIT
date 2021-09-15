namespace LionTrust.Feature.Sitemap.Models
{
    using System.Collections.Generic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;

    public class IndexedItem : SearchResultItem
    {
        [IndexField(Constants.IndexFields.IncludeInSitemap)]
        public bool IncludeInSitemap { get; set; }

        [IndexField(Constants.IndexFields.AllTemplates)]
        public List<string> AllTemplates { get; set; }
    }
}