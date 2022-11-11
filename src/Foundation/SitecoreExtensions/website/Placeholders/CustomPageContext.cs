using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Sitecore.SecurityModel;
using Sitecore.Web;

namespace LionTrust.Foundation.SitecoreExtensions.Placeholders
{
    public class CustomPageContext : PageContext
    {
        public new Item GetPlaceholderItem(string placeholderKey, Database database, string layoutDefinition)
        {
            Assert.ArgumentNotNull(placeholderKey, nameof(placeholderKey));
            Assert.ArgumentNotNull(database, nameof(database));
            Assert.ArgumentNotNull(layoutDefinition, nameof(layoutDefinition));

            placeholderKey = placeholderKey.ToLowerInvariant();
            var placeholderDefinition = GetPlaceholderDefinition(layoutDefinition, placeholderKey);

            if (placeholderDefinition != null)
            {
                var metaDataItemId = placeholderDefinition.MetaDataItemId;
                if (string.IsNullOrEmpty(metaDataItemId))
                {
                    return null;
                }

                using (new SecurityDisabler())
                {
                    return DatabaseHelper.GetItemByPathOrId(database, metaDataItemId);
                }
            }
            else
            {
                //get placeholders based on site
                var placeholderItem = GetSitePlaceholderItem(placeholderKey, database);
                return placeholderItem;
            }
        }

        private Item GetSitePlaceholderItem(string placeholderKey, Database database)
        {
            // get placeholder item based on site
            var sitePlaceholderCache = CustomPlaceholderCacheManager.GetPlaceholderCache(database.Name, GetSiteName());
            var placeholderItem = GetPlaceholderItem(placeholderKey, sitePlaceholderCache);
            if (placeholderItem != null)
            {
                return placeholderItem;
            }

            //get placeholder item under Feature folder
            var featurePlaceholder = CustomPlaceholderCacheManager.GetPlaceholderCache(database.Name, Constants.PlaceholderSettings.FeatureFolder_ID);
            placeholderItem = GetPlaceholderItem(placeholderKey, featurePlaceholder);
            if (placeholderItem != null)
            {
                return placeholderItem;
            }

            //get placeholder item under Foundation folder
            var foundationPlaceholder = CustomPlaceholderCacheManager.GetPlaceholderCache(database.Name, Constants.PlaceholderSettings.FoundationFolder_ID);
            placeholderItem = GetPlaceholderItem(placeholderKey, foundationPlaceholder);

            return placeholderItem;
        }

        private Item GetPlaceholderItem(string placeholderKey, CustomPlaceholderCache placeholderCache)
        {
            Item placeholderItem = null;

            if (placeholderCache != null)
            {
                placeholderItem = placeholderCache[placeholderKey];
                if (placeholderItem != null)
                {
                    return placeholderItem;
                }

                var num1 = placeholderKey.LastIndexOf('/');
                if (num1 >= 0)
                {
                    var key = StringUtil.Mid(placeholderKey, num1 + 1);
                    placeholderItem = placeholderCache[key];
                }
            }

            return placeholderItem;
        }

        private string GetSiteName()
        {
            var siteName = WebUtil.GetSafeQueryString(Constants.SiteCookieName);
            if (!string.IsNullOrEmpty(siteName))
            {
                return siteName;
            }

            var pageSite = WebUtil.GetSafeQueryString(Constants.PageSiteCookieName);
            if (!string.IsNullOrEmpty(pageSite))
            {
                return pageSite;
            }

            return Client.Site?.Name; 
        }
    }
}