namespace LionTrust.Foundation.Indexing.ComputedFields.GenericListingModuleItem
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Fields;

    public class ImageWidth : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);

            if (string.IsNullOrEmpty(item[Legacy.Constants.GenericListingModuleItem.Image_FieldID]))
            {
                return string.Empty;
            }

            ImageField image = item?.Fields[Legacy.Constants.GenericListingModuleItem.Image_FieldID];

            return image.Width;
        }
    }
}