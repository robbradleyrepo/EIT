namespace LionTrust.Foundation.Indexing.ComputedFields.GenericListingModuleItem
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

    public class ImageProtected : IComputedIndexField
    {
        public string FieldName { get; set; }
        
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            var publishedDatabase = Sitecore.Data.Database.GetDatabase("web");

            if (!string.IsNullOrEmpty(item[Legacy.Constants.GenericListingModuleItem.Image_FieldID]))
            {
                ImageField image = item?.Fields[Legacy.Constants.GenericListingModuleItem.Image_FieldID];

                MediaItem mediaItem;
                if (image?.MediaDatabase.Name == "shell")
                {
                    mediaItem = publishedDatabase.GetItem(image.MediaID);
                }
                else
                {
                    var database =
                            image != null && image.MediaDatabase != null && image.MediaDatabase.Name != "shell"
                                    ? image.MediaDatabase
                                    : publishedDatabase;

                    mediaItem = image?.MediaItem ?? database.GetItem(image.MediaID);
                }

                if (mediaItem == null)
                {
                    return string.Empty;
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