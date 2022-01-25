namespace LionTrust.Feature.Fund.Literature
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;

    public class DocumentSearchResult: SearchResultItem
    {
        [IndexField("alltemplates_sm")]
        public string[] AllTemplates { get; set; }

        [IndexField("legacydocument_relatedfunds_sm")]
        public string[] RelatedFunds { get; set; }
    }
}