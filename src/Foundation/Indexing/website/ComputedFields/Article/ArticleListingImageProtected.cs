namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using System;
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;

    public class ArticleListingImageProtected : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }
        
        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);

            ImageField imageField = item?.Fields[Legacy.Constants.Article.Article_ListingImage];
            MediaItem mediaItem;
            if(imageField?.MediaDatabase.Name == "shell")
            {
                var publishedDatabase = Sitecore.Data.Database.GetDatabase("web");
                mediaItem = publishedDatabase.GetItem(imageField.MediaID);
            }
            else
            {
                mediaItem = imageField?.MediaItem ?? imageField.MediaDatabase.GetItem(imageField.MediaID);
            }

            string hashedUrl = string.Empty;
            if (mediaItem != null)
            {
                var imageUrl = MediaManager.GetMediaUrl(mediaItem, new MediaUrlOptions() { AlwaysIncludeServerUrl = false, Database = mediaItem.Database, LowercaseUrls = true });
                hashedUrl = imageUrl != null ? HashingUtils.ProtectAssetUrl(imageUrl) : string.Empty;
            }

            return hashedUrl;
        }
    }
}