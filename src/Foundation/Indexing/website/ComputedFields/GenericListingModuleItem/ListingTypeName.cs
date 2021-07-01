namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;

    public class ListingTypeName : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }
        
        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);

            if (!string.IsNullOrEmpty(item[Legacy.Constants.GenericListingModuleItem.ListingType_FieldID])) 
            {
                var listingTypeItem = item.Database.GetItem(item[Legacy.Constants.GenericListingModuleItem.ListingType_FieldID]);
                if (listingTypeItem != null)
                {
                    return listingTypeItem[Legacy.Constants.ListingItemType.ListingItemTypeName_FieldId];
                }
            }

            return string.Empty;
        }
    }
}