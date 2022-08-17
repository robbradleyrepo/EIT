namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;

    public class ArticleFundName : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }
        
        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);

            var multiValueField = item?.Fields[Legacy.Constants.Article.Fund_FieldId];

            return multiValueField != null
                                    ? ComputedValueHelper.GetMultiListValue(multiValueField, "LegacyFund_FundName")
                                    : null;
        }
    }
}