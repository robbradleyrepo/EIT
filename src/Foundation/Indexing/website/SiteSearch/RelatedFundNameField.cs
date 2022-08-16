namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using System.Linq;

    public class RelatedFundNameField : IComputedIndexField
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

            return multiValueField != null
                                    ? ComputedValueHelper.GetMultiListValue(multiValueField, "LegacyFund_FundName")
                                    : null;
        }
    }
}