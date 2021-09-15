namespace LionTrust.Feature.Sitemap.Pipelines
{
    using System.Collections.Generic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Data;
    
    internal class SitemapResultItem : SearchResultItem
  {
    [IndexField("_templates")]
    public IEnumerable<ID> AllTemplates { get; set; }
  }
}