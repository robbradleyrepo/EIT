namespace LionTrust.Foundation.Indexing.ComputedFields.GenericListingModuleItem
{
    using System;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Web.UI.XslControls;

    public class Text : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);

            if (!string.IsNullOrEmpty(item[Legacy.Constants.GenericListingModuleItem.Text_FieldID]))
            {
                return Sitecore.Web.UI.WebControls.FieldRenderer.Render(item, Legacy.Constants.GenericListingModuleItem.Text_FieldName);
            }

            return string.Empty;
        }
    }
}