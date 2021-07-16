namespace LionTrust.Foundation.Indexing.ComputedFields.GenericListingModuleItem
{
    using System;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Sites;

    public class Text : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            if (item != null && !string.IsNullOrEmpty(item[Legacy.Constants.GenericListingModuleItem.Text_FieldName]) && item[Legacy.Constants.GenericListingModuleItem.Text_FieldName].Contains("Download"))
            {
                try
                {
                    using (new SiteContextSwitcher(Factory.GetSite(Constants.SiteName)))
                    {
                        return Sitecore.Web.UI.WebControls.FieldRenderer.Render(item, Legacy.Constants.GenericListingModuleItem.Text_FieldName);
                    }
                }
                catch
                {
                }
            }

            return string.Empty;
        }
    }
}