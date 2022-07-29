namespace LionTrust.Foundation.SitecoreExtensions.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Abstractions;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;
    using Sitecore.Diagnostics;
    using Sitecore.Links;
    using Sitecore.Publishing;
    using Sitecore.Resources.Media;

    public static class ItemExtensions
    {
        public static string Url(this Item item, UrlOptions options = null)
        {
            if (item == null)
            {
                return string.Empty;
            }

            if (options != null)
            {
                return LinkManager.GetItemUrl(item, options);
            }
            return !item.Paths.IsMediaItem ? LinkManager.GetItemUrl(item) : MediaManager.GetMediaUrl(item);
        }

        public static string GetCanonicalUrl(this Item item)
        {
            UrlOptions urlOptions = LinkManager.GetDefaultUrlOptions();
            urlOptions.LowercaseUrls = true;
            urlOptions.AlwaysIncludeServerUrl = true;
            urlOptions.LanguageEmbedding = LanguageEmbedding.Never;
            urlOptions.SiteResolving = Settings.Rendering.SiteResolving;

            string url = LinkManager.GetItemUrl(item, urlOptions);
            
            return url;
        }

        public static Item GetAncestorOrSelfOfTemplate(this Item item, ID templateID)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return item.DescendsFrom(templateID) ? item : item.Axes.GetAncestors().LastOrDefault(i => i.DescendsFrom(templateID));
        }

        public static bool HasContextLanguage(this Item item)
        {
            var latestVersion = item.Versions.GetLatestVersion();
            return latestVersion?.Versions.Count > 0;
        }

        public static bool HasLayout(this Item item)
        {
            return item?.Visualization?.Layout != null;
        }

        public static IEnumerable<Item> GetMultiListValueItems(this Item item, ID fieldId)
        {
            return new MultilistField(item.Fields[fieldId]).GetItems();
        }

        public static Item GetDropLinkValueItem(this Item item, ID fieldId)
        {
            if (item[fieldId] != string.Empty)
            {
                return new Sitecore.Data.Fields.DatasourceField(item.Fields[fieldId]).TargetItem;
            }
            else
            {
                return null;
            }
        }

        public static bool HasComponent(this Item item, string renderingId)
        {
            if (item == null)
            {
                return false;
            }

            DeviceItem device = Sitecore.Context.Device;
            var renderings = item.Visualization?.GetRenderings(device, true);

            if (renderings == null)
            {
                return false;
            }

            return renderings.Any(r => r.RenderingID == new ID(renderingId));
        }

        #region "Publish item"

        /// <summary>
        /// Publishes an item with default options, from master to web
        /// </summary>
        /// <param name="item">Item to be published</param>
        public static void PublishItem(this Item item)
        {
            PublishItem(item, false);
        }

        /// <summary>
        /// Publishes an item and its subitems, a SmartPublish with subitems
        /// </summary>
        /// <param name="item">Item to be published</param>
        /// <param name="includeSubitems">should subitems also be published</param>
        public static void PublishItem(this Item item, bool includeSubitems)
        {
            var sourceDatabase = Sitecore.Configuration.Factory.GetDatabase("master");
            var targetDatabase = Sitecore.Configuration.Factory.GetDatabase("web");
            PublishItem(item, includeSubitems, sourceDatabase, targetDatabase, false);
        }

        /// <summary>
        /// Publishes an item with default options, from master to web, in async mode
        /// </summary>
        /// <param name="item">Item to be published</param>
        /// <returns>a Job item containing the publisher</returns>
        public static BaseJob PublishItemAsync(this Item item)
        {
            return PublishItemAsync(item, false);
        }

        /// <summary>
        /// Publishes an item and its subitems, a SmartPublish with subitems, in async mode
        /// </summary>
        /// <param name="item">Item to be published</param>
        /// <param name="includeSubitems">should subitems also be published</param>
        /// <returns>a Job containing the publisher</returns>
        public static BaseJob PublishItemAsync(this Item item, bool includeSubitems)
        {
            var sourceDatabase = Factory.GetDatabase("master");
            var targetDatabase = Factory.GetDatabase("web");
            return PublishItem(item, includeSubitems, sourceDatabase, targetDatabase, true);
        }

        /// <summary>
        /// Most general mothod for pubishing an item
        /// </summary>
        /// <param name="item">the item to publish</param>
        /// <param name="includeSubitems">should subitems also be published</param>
        /// <param name="sourceDatabase">from this database</param>
        /// <param name="targetDatabase">to this database</param>
        /// <param name="async">should the publish be async</param>
        /// <returns>a Job item containing the publisher, if async is true</returns>
        public static BaseJob PublishItem(this Item item, bool includeSubitems, Database sourceDatabase, Database targetDatabase, bool async)
        {
            Assert.IsNotNull(item, "item is null");
            Assert.IsNotNull(sourceDatabase, "sourceDatabase is null");
            Assert.IsNotNull(targetDatabase, "targetDatabase is null");

            var options = new PublishOptions(sourceDatabase, targetDatabase, PublishMode.SingleItem,
                                             LanguageManager.DefaultLanguage, DateTime.Now);

            options.Deep = includeSubitems;

            options.CompareRevisions = true;
            options.RootItem = item;

            var publisher = new Publisher(options);
            if (!async)
            {
                publisher.Publish();
                return null;
            }
            else
            {
                return publisher.PublishAsync();
            }
        }

        #endregion
    }
}