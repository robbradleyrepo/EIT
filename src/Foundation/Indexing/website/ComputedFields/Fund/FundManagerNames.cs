namespace LionTrust.Foundation.Indexing.ComputedFields.Fund
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;

    public class FundManagerNames : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            var fundManagers = item.Fields[Legacy.Constants.Fund.FundManagersFieldId];
            var fundManagerNames = ComputedValueHelper.GetMultiListValue(fundManagers, Legacy.Constants.Author.FullName_FieldName);

            return fundManagerNames;
        }
    }
}

