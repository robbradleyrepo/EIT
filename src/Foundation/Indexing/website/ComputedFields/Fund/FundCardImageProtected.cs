namespace LionTrust.Foundation.Indexing.ComputedFields.Fund
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

    public class FundCardImageProtected : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            var publishedDatabase = Database.GetDatabase("web");
            var fundCardImage = (ImageField)item.Fields[Constants.FundCardImage_FieldId];
            var hashedUrl = string.Empty;

            if (fundCardImage != null)
            {
                MediaItem mediaItem;
                if (fundCardImage.MediaDatabase.Name == "shell")
                {
                    mediaItem = publishedDatabase.GetItem(fundCardImage.MediaID);
                }
                else
                {
                    var database =
                            fundCardImage != null && fundCardImage.MediaDatabase != null && fundCardImage.MediaDatabase.Name != "shell"
                                    ? fundCardImage.MediaDatabase
                                    : publishedDatabase;

                    mediaItem = fundCardImage?.MediaItem ?? database.GetItem(fundCardImage.MediaID);
                }

                if (mediaItem != null)
                {
                    var mediaOption = new MediaUrlOptions() { AlwaysIncludeServerUrl = false, AbsolutePath = true, Database = mediaItem.Database, LowercaseUrls = true };
                    using (new SiteContextSwitcher(Factory.GetSite(Constants.SiteName)))
                    {
                        var imageUrl = MediaManager.GetMediaUrl(mediaItem, mediaOption);
                        hashedUrl = imageUrl != null ? HashingUtils.ProtectAssetUrl(imageUrl) : string.Empty;
                    }
                }
            }

            return hashedUrl;
        }
    }
}
          
        
    
