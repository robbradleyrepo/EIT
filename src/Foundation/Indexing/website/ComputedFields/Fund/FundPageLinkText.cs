namespace LionTrust.Foundation.Indexing.ComputedFields.Fund
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Fields;

    public class FundPageLinkText : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            var fundPageText = string.Empty;
            var fundPageField = (LinkField)item.Fields[Constants.FundPageFieldId];

            if (fundPageField != null && !string.IsNullOrWhiteSpace(fundPageField.Text))
            {
                fundPageText = fundPageField.Text;
            }

            return fundPageText;
        }
    }
}
    
