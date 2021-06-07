namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using System;

    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Items;

    public class ArticleTopicNames : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }
        
        public object ComputeFieldValue(IIndexable indexable)
        {
            if (indexable == null)
            { 
                throw new ArgumentNullException("indexable");
            }

            var scIndexable = indexable as SitecoreIndexableItem;

            if (scIndexable == null)
            {
                Sitecore.Diagnostics.Log.Warn(
                    this + " : unsupported IIndexable type : " + indexable.GetType(), this);
                return false;
            }

            var item = (Item)scIndexable;
            if (item == null)
            {
                Sitecore.Diagnostics.Log.Warn(
                    this + " : unsupported SitecoreIndexableItem type : " + scIndexable.GetType(), this);
                return false;
            }

            if (string.Compare(item.Database.Name, "core", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return false;
            }

            var multiValueField = item.Fields[Legacy.Constants.Article.Topics_FieldId];

            return multiValueField != null
                                    ? ComputedValueHelper.GetMultiListValue(multiValueField, "Topic_Value")
                                    : null;
        }
    }
}