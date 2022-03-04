namespace LionTrust.Foundation.Indexing.ComputedFields.GenericListingModuleItem
{
    using System;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Fields;

    public class Month : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            if (item == null)
            {
                return null;
            }

            DateField dateTimeField = item.Fields[Constants.GenericListingItemDate_FieldId];
            return ComputedValueHelper.GetDateTimeFieldValue(dateTimeField, item.Created).Month;                      
        }
    }
}