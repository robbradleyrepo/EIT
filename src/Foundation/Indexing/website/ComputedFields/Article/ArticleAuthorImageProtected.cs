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
    using System.Linq;

    public class ArticleAuthorImageProtected : IComputedIndexField
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

            if (!string.IsNullOrEmpty(item[Legacy.Constants.Article.Authors_FieldId]))
            {
                ImageField authorImage;
                var authorsIds = item[Legacy.Constants.Article.Authors_FieldId].Split('|').Where(x => !string.IsNullOrEmpty(x));
                if (authorsIds.Count() > 1)
                {
                    authorImage = item?.Fields[Legacy.Constants.Article.MultipleAuthorsIcon_FieldId];                    
                }
                else
                {
                    var author = item.Database.GetItem(authorsIds.FirstOrDefault());
                    authorImage = author?.Fields[Legacy.Constants.Author.Image_FieldId];                    
                }

                MediaItem mediaItem;
                if (authorImage?.MediaDatabase.Name == "shell")
                {
                    mediaItem = publishedDatabase.GetItem(authorImage.MediaID) ?? GetDefaultListingImage(publishedDatabase);
                }
                else
                {
                    var database =
                            authorImage != null && authorImage.MediaDatabase != null && authorImage.MediaDatabase.Name != "shell"
                                    ? authorImage.MediaDatabase
                                    : publishedDatabase;

                    mediaItem = authorImage?.MediaItem ?? database.GetItem(authorImage.MediaID);
                    if (mediaItem == null)
                    {
                        mediaItem = GetDefaultListingImage(database);
                    }
                }

                var hashedUrl = string.Empty;
                var mediaOption = new MediaUrlOptions() { AlwaysIncludeServerUrl = false, AbsolutePath = true, Database = mediaItem.Database, LowercaseUrls = true };
                using (new SiteContextSwitcher(Factory.GetSite(Constants.SiteName)))
                {
                    var imageUrl = MediaManager.GetMediaUrl(mediaItem, mediaOption);
                    hashedUrl = imageUrl != null ? HashingUtils.ProtectAssetUrl(imageUrl) : string.Empty;
                }

                return hashedUrl;
            }
            else
            {   
                return string.Empty;
            }            
        }
    }
}