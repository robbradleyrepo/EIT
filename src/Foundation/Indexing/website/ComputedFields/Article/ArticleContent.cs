namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using System;
    using System.Linq;
    using System.Text;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data;

    public class ArticleContent : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }

        public static string LocalDatasourceFolderTemplateId => Sitecore.Configuration.Settings.GetSetting("Foundation.LocalDatasource.LocalDatasourceFolderTemplate");

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);

            if (item == null || !item.HasChildren)
            {
                return null;
            }
            
            var localFolder = item.GetChildren().Where(c => c.TemplateID == new ID(LocalDatasourceFolderTemplateId))?.First();
            if (localFolder == null || !localFolder.HasChildren)
            {
                return null;
            }

            var content = new StringBuilder("");
            var contentItems = localFolder.GetChildren().Where(c => c.TemplateID == new ID(Constants.ArticleRichTextTemplateId));
            if (contentItems != null)
            {
                foreach (var contentItem in contentItems)
                {
                    if (contentItem != null)
                    {
                        content.Append(contentItem.Fields[new ID(Constants.ArticleRichTextCopy_FieldId)].Value);
                        content.AppendLine();
                    }
                }
            }

            return content.ToString();
        }
    }
}