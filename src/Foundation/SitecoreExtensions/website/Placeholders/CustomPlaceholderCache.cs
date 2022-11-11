using Sitecore;
using Sitecore.Caching.Placeholders;
using Sitecore.Data;

namespace LionTrust.Foundation.SitecoreExtensions.Placeholders
{
    public class CustomPlaceholderCache : PlaceholderCache
    {
        public CustomPlaceholderCache(string databaseName)
          : base(databaseName)
        {
            ItemRootId = ItemIDs.PlaceholderSettingsRoot;
        }

        public CustomPlaceholderCache(string databaseName, ID itemRootId)
          : base(databaseName)
        {
            ItemRootId = itemRootId;
        }

        public CustomPlaceholderCache(string databaseName, string siteName)
          : base(databaseName)
        {
            switch (siteName)
            {
                case Constants.SiteNames.LionTrust:
                    ItemRootId = Constants.PlaceholderSettings.LionTrustFolder_ID;
                    break;
                case Constants.SiteNames.EIT:
                    ItemRootId = Constants.PlaceholderSettings.EITFolder_ID;
                    break;
                default:
                    ItemRootId = ItemIDs.PlaceholderSettingsRoot;
                    break;
            }
        }

        public override ID ItemRootId { get; }
    }
}