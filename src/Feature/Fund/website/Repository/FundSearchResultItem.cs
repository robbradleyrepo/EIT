namespace LionTrust.Feature.Fund.Repository
{
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using Sitecore.ContentSearch;

    public class FundSearchResultItem : BaseSearchResultItem
    {
        [IndexField("fund_classes_sm")]
        public string[] FundClasses { get; set; }
    }
}