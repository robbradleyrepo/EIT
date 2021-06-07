namespace LionTrust.Feature.Fund.Repository
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;
    using System.Collections.Generic;

    public class FundSearchResultItem: SearchResultItem
    {
        [IndexField("fund_classes_sm")]
        public string[] FundClasses { get; set; }
    }
}