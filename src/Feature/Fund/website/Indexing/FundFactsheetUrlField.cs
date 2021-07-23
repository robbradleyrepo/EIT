namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using Sitecore.Abstractions;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;
    using Sitecore.Sites;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(IFundFactsheetUrlField), Lifetime = Lifetime.Singleton)]
    public class FundFactsheetUrlField : IFundFactsheetUrlField
    {
        private readonly BaseFactory _factory;

        public FundFactsheetUrlField(BaseFactory factory)
        {
            _factory = factory;
        }

        public bool CanHandle(IEnumerable<System.Guid> templateIds)
        {
            return templateIds.Contains(Constants.FundPage.TemplateId);
        }

        public string GetFundFactsheetUrl(Item item)
        {
            if (item == null)
            {
                return null;
            }

            var fundField = (LookupField)item.Fields[Constants.FundSelector.FundFieldId];
            if (fundField == null || fundField.TargetItem == null)
            {
                return null;
            }

            var publishedDatabase = Sitecore.Data.Database.GetDatabase("web");
            var CitiCode = fundField.TargetItem.Fields[Foundation.Legacy.Constants.Fund.CitiCodeFieldId];
            var hashedUrl = string.Empty;

            if (CitiCode != null && CitiCode.HasValue)
            {
                var fundClassesItem = item.Database.GetItem(new ID(Foundation.Indexing.Constants.FundClassesItemId));
                if (fundClassesItem != null && fundClassesItem.HasChildren)
                {
                    var fundClass = fundClassesItem.Children.FirstOrDefault(c => c.Fields[Foundation.Legacy.Constants.FundClass.CitiCodeFieldId].HasValue
                     && c.Fields[Foundation.Legacy.Constants.FundClass.CitiCodeFieldId].Value == CitiCode.Value);

                    if (fundClass != null)
                    {
                        var factSheet = (FileField)fundClass.Fields[Foundation.Legacy.Constants.FundClass.FactsheetFieldId];

                        if (!string.IsNullOrWhiteSpace(factSheet.Value))
                        {
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
                                using (new SiteContextSwitcher(_factory.GetSite(Foundation.Indexing.Constants.SiteName)))
                                {
                                    var imageUrl = MediaManager.GetMediaUrl(mediaItem, mediaOption);
                                    hashedUrl = imageUrl != null ? HashingUtils.ProtectAssetUrl(imageUrl) : string.Empty;
                                }
                            }
                        }
                    }
                }
            }
            return hashedUrl;
        }
    }
}