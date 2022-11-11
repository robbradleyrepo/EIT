using Sitecore.Diagnostics;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System.Collections.Concurrent;
using Sitecore.Data;

namespace LionTrust.Foundation.SitecoreExtensions.Placeholders
{
    public class CustomBasePlaceholderCacheManager
    {
        private readonly ConcurrentDictionary<string, CustomPlaceholderCache> caches = new ConcurrentDictionary<string, CustomPlaceholderCache>();

        public void Clear() => caches.Clear();

        public CustomPlaceholderCache GetPlaceholderCache(string databaseName, string siteName)
        {
            Assert.ArgumentNotNull(databaseName, nameof(databaseName));
            Assert.ArgumentNotNull(siteName, nameof(siteName));

            return caches.GetOrAdd($"{databaseName}-{siteName}", x => InstantiateCache(databaseName, siteName));
        }

        public CustomPlaceholderCache GetPlaceholderCache(string databaseName, ID rootId)
        {
            Assert.ArgumentNotNull(databaseName, nameof(databaseName));
            Assert.ArgumentNotNull(rootId, nameof(rootId));

            return caches.GetOrAdd($"{databaseName}-{rootId.Guid}", x => InstantiateCache(databaseName, rootId));
        }

        public void UpdateCache(Item item, string siteName)
        {
            if (item == null)
            {
                return;
            }

            caches.GetOrAdd($"{item.Database.Name}-{siteName}", x => InstantiateCache(item.Database.Name, siteName))?.UpdateCache(item);
        }

        public void UpdateCache(Item item, ID rootId)
        {
            if (item == null)
            {
                return;
            }

            caches.GetOrAdd($"{item.Database.Name}-{rootId.Guid}", x => InstantiateCache(item.Database.Name, rootId))?.UpdateCache(item);
        }

        private CustomPlaceholderCache InstantiateCache(string databaseName, string siteName)
        {
            var placeholderCache = new CustomPlaceholderCache(databaseName, siteName);
            using (new SecurityDisabler())
            {
                if (Factory.GetDatabase(databaseName).GetItem(placeholderCache.ItemRootId) == null)
                {
                    return null;
                }
            }

            placeholderCache.Reload();
            return placeholderCache;
        }

        private CustomPlaceholderCache InstantiateCache(string databaseName, ID rootId)
        {
            CustomPlaceholderCache placeholderCache = new CustomPlaceholderCache(databaseName, rootId);
            using (new SecurityDisabler())
            {
                if (Factory.GetDatabase(databaseName).GetItem(placeholderCache.ItemRootId) == null)
                {
                    return null;
                }
            }

            placeholderCache.Reload();
            return placeholderCache;
        }
    }
}
