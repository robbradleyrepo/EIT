namespace LionTrust.Foundation.Indexing.ComputedFields.Article
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;
    using Sitecore.Sites;

    public class ArticleListingImageProtected : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }

        private MediaItem GetDefaultListingImage(Database database)
        {
            return database.GetItem(Constants.DefaultListingImagePath);
        }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            var publishedDatabase = Sitecore.Data.Database.GetDatabase("web");

            ImageField imageField = item?.Fields[Legacy.Constants.Article.Article_ListingImage];
            MediaItem mediaItem;

            var database = 
                    imageField != null && imageField.MediaDatabase != null && imageField.MediaDatabase.Name != "shell"
                            ? imageField.MediaDatabase 
                            : publishedDatabase;

            mediaItem = imageField?.MediaItem ?? database.GetItem(imageField.MediaID);
            if(mediaItem == null)
            {
                mediaItem = GetDefaultListingImage(database);
            }
            

            var hashedUrl = string.Empty;
            if (mediaItem != null)
            {
                var mediaOption = new MediaUrlOptions() { AlwaysIncludeServerUrl = false, AbsolutePath = true, Database = mediaItem.Database, LowercaseUrls = true };
                using (new SiteContextSwitcher(Factory.GetSite(Constants.SiteName)))
                {
                    var imageUrl = MediaManager.GetMediaUrl(mediaItem, mediaOption);
                    hashedUrl = imageUrl != null ? HashingUtils.ProtectAssetUrl(imageUrl) : string.Empty;
                }
                
            }

            return hashedUrl;
        }
    }
}