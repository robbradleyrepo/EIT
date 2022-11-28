namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Links;
    using Sitecore.Sites;
    using System.Linq;

    public class RelatedFundUrlField : IComputedIndexField
    {       
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var indexItem = indexable as SitecoreIndexableItem;

            if (indexItem == null)
            {
                return null;
            }

            var item = indexItem.Item;
            if (!item.Template.BaseTemplates.Any(t => t.ID.ToString() == Legacy.Constants.Document.TemplateId))
            {
                return null;
            }

            var multiValueField = item?.Fields[Legacy.Constants.Document.RelatedFunds_FieldId];
            var ids = multiValueField != null ? ComputedValueHelper.ExtractIds(multiValueField) : null;
            if (ids == null)
            {
                return null;
            }

            var fundItem = item.Database.GetItem(new ID(ids.FirstOrDefault()));
            if (fundItem == null)
            {
                return null;
            }

            var fundPageUrl = string.Empty;
            var fundPageField = (LinkField)fundItem.Fields[Constants.FundPageFieldId];

            if (fundPageField != null && fundPageField.TargetItem != null)
            {
                fundPageUrl = LinkManager.GetItemUrl(fundPageField.TargetItem, new UrlOptions { AlwaysIncludeServerUrl = true, LowercaseUrls = true, LanguageEmbedding = LanguageEmbedding.Never, SiteResolving = true });
            }

            return fundPageUrl;
        }
    }
}