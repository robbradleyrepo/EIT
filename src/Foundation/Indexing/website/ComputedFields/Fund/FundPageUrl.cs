namespace LionTrust.Foundation.Indexing.ComputedFields.Fund
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Fields;
    using Sitecore.Links;
    using Sitecore.Sites;

    public class FundPageUrl : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            var fundPageUrl = string.Empty;
            var fundPageField = (LinkField)item.Fields[Constants.FundPageFieldId];

            if (fundPageField != null && fundPageField.TargetItem != null)
            {
                using (new SiteContextSwitcher(Factory.GetSite(Constants.SiteName)))
                {
                    fundPageUrl = LinkManager.GetItemUrl(fundPageField.TargetItem, new UrlOptions { AlwaysIncludeServerUrl = true, LowercaseUrls = true });
                }
            }

            return fundPageUrl;
        }
    }
}