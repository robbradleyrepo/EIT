using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;

namespace LionTrust.Foundation.SitecoreExtensions.Placeholders
{
    public class CustomPlaceholderCacheManager
    {
        private static readonly LazyResetable<CustomBasePlaceholderCacheManager> Instance = ServiceLocator.GetRequiredResetableService<CustomBasePlaceholderCacheManager>();

        public static void Clear() => Instance.Value.Clear();

        public static CustomPlaceholderCache GetPlaceholderCache(string databaseName, string siteName)
        {
            Assert.ArgumentNotNull((object)databaseName, nameof(databaseName));

            return Instance.Value.GetPlaceholderCache(databaseName, siteName);
        }

        public static CustomPlaceholderCache GetPlaceholderCache(string databaseName, ID rootId)
        {
            Assert.ArgumentNotNull((object)databaseName, nameof(databaseName));

            return Instance.Value.GetPlaceholderCache(databaseName, rootId);
        }

        public static void UpdateCache(Item item, string siteName) => Instance.Value.UpdateCache(item, siteName);

        public static void UpdateCache(Item item, ID rootId) => Instance.Value.UpdateCache(item, rootId);
    }
}
