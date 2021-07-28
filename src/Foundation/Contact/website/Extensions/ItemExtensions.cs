namespace LionTrust.Foundation.Contact.Extensions
{
    using System;
    using System.Collections.Generic;
    using Codehouse.Common;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;
    using Sitecore.Diagnostics;
    using Sitecore.Links;
    using Sitecore.Publishing;
    using Sitecore.Resources.Media;
    using Sitecore.Abstractions;
    using LionTrust.Foundation.Contact.Models.Types;

    public static class ItemExtensions
    {
        public static IProperty<Image> GetImage(this Item item, string fieldName, int maxWidth = 0, int maxHeight = 0)
        {
            ImageField imageField = item.Fields[fieldName];
            var image = new Image();

            if (imageField != null && imageField.MediaItem != null)
            {
                image.Path = MediaManager.GetMediaUrl(imageField.MediaItem);
                image.MaxHeight = maxHeight;
                image.MaxWidth = maxWidth;
            }
            return new SitecoreImage(item, fieldName, image);
        }

        public static IProperty<Link> GetLink(this Item item, string fieldName, IDictionary<string, string> renderingParameters, int maxWidth = 0, int maxHeight = 0)
        {
            var iLink = GetLink(item, fieldName, maxWidth, maxHeight);

            if (renderingParameters == null)
            {
                return iLink; 
            }

            string buttonStyleSettings;
            if (!renderingParameters.TryGetValue("ButtonStyle", out buttonStyleSettings))
            {
                return iLink;
            }

            var buttonStyleLookupValue = Sitecore.Context.Database.GetItem(new ID(new Guid(buttonStyleSettings)));
            iLink.Value.Css = buttonStyleLookupValue.Fields["Value"].Value ?? "cta";

            return iLink;
        }

        public static IProperty<Link> GetLink(this Item item, string fieldName, int maxWidth = 0, int maxHeight = 0)
        {
            LinkField linkField = item.Fields[fieldName];
            var link = new Link();

            if (linkField != null)
            {
                link.Text = linkField.Text;
                link.Url = linkField.Url;
                link.IsInternal = linkField.IsInternal;
                //Internal link fix
                if (linkField.IsInternal && linkField.TargetItem != null)
                {
                    link.Url = LinkManager.GetItemUrl(linkField.TargetItem);
                    link.TargetItemId = linkField.TargetItem.ID.Guid;
                }
                else if (linkField.IsMediaLink)
                {
                    link.Url = MediaManager.GetMediaUrl(linkField.TargetItem);
                }
            }
            return new SitecoreLink(item, fieldName, link);
        }

        public static string GetReferencedItemString(this Item item, string referenceFieldName, string fieldName)
        {
            string result = String.Empty;

            var resultItem = item.GetReferenceFieldValue(referenceFieldName);

            if (resultItem != null)
            {
                result = resultItem[fieldName];
            }

            return result;
        }

        public static IProperty<string> GetIPropertyString(this Item item, string fieldName)
        {
            return new SitecoreText(item, fieldName, item[fieldName]);
        }

        public static Guid GetGuid(this Item item, string fieldName)
        {
            var result = item[fieldName];

            if (result == string.Empty)
            {
                return  new Guid();
            }
            return new Guid(result);
        }

        public static DropLink GetDropLinkItem(this Item item, string fieldName)
        {
            ReferenceField dropLinkField = item.Fields[fieldName];

            var dropLink = new DropLink();

            if (dropLinkField == null || dropLinkField.TargetItem == null)
            {
                return dropLink;
            }
       
            dropLink.ItemId = dropLinkField.TargetID;
            dropLink.ItemName = dropLinkField.TargetItem.Name ?? string.Empty;
            dropLink.SitecoreItem = dropLinkField.TargetItem;

            return dropLink;
        }

        public static ItemProperty GetItemProperty(this Item item)
        {
            if (item == null)
            {
                return null;
            }
            return new ItemProperty
            {
                FullPath = item.Paths.FullPath,
                LongId = item.Paths.LongID,
                Name = item.Name,
                SitecoreId = item.ID.Guid,
                TemplateId = item.TemplateID.Guid
            };
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
            var sourceDatabase = Sitecore.Configuration.Factory.GetDatabase("master");
            var targetDatabase = Sitecore.Configuration.Factory.GetDatabase("web");
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
