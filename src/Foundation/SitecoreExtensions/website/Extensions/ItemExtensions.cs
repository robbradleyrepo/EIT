namespace LionTrust.Foundation.SitecoreExtensions.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Links;
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
            // Copy option from setting
            urlOptions.SiteResolving = Settings.Rendering.SiteResolving;
            // If we ever include the language in the path,
            // then we always include the language in the canonical URL.
            if (urlOptions.LanguageLocation == LanguageLocation.FilePath
                && urlOptions.LanguageEmbedding != LanguageEmbedding.Never)
            {
                urlOptions.LanguageEmbedding = LanguageEmbedding.Always;
            }
            string url = LinkManager.GetItemUrl(item, urlOptions);
            // Don't include /language.aspx for home pages.
            if (urlOptions.LanguageEmbedding == LanguageEmbedding.Always)
            {
                string[] parsed = url.Split('/');
                if (parsed.Length == 4)
                {
                    url = string.Join("/", new[] { parsed[0], parsed[1], parsed[2] }) + '/';
                }
            }

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
    }
}