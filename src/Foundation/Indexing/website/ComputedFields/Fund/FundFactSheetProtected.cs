

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

    public class FundFactSheetProtected : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            var publishedDatabase = Sitecore.Data.Database.GetDatabase("web");
            var CitiCode = item.Fields[Legacy.Constants.Fund.CitiCodeFieldId];
            var hashedUrl = string.Empty;

            if (CitiCode.HasValue)
            {
                var fundClassesItem = item.Database.GetItem(new ID(Constants.FundClassesItemId));
                if (fundClassesItem != null && fundClassesItem.HasChildren)
                {
                    var fundClass = fundClassesItem.Children.FirstOrDefault(c => c.Fields[Legacy.Constants.FundClass.CitiCodeFieldId].HasValue
                     && c.Fields[Legacy.Constants.FundClass.CitiCodeFieldId].Value == CitiCode.Value);

                    if (fundClass != null)
                    {
                        var factSheet = (ImageField)fundClass.Fields[Legacy.Constants.FundClass.FactsheetFieldId];

                        MediaItem mediaItem;
                        if (factSheet?.MediaDatabase.Name == "shell")
                        {
                            mediaItem = publishedDatabase.GetItem(factSheet.MediaID);
                        }
                        else
                        {
                            var database =
                                    factSheet != null && factSheet.MediaDatabase != null && factSheet.MediaDatabase.Name != "shell"
                                            ? factSheet.MediaDatabase
                                            : publishedDatabase;

                            mediaItem = factSheet?.MediaItem ?? database.GetItem(factSheet.MediaID);

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
                }
            }
                return hashedUrl; 
        }
    }
}
    
