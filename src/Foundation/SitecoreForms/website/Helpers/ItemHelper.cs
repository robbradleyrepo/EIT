namespace LionTrust.Foundation.SitecoreForms.Helpers
{
    using System;
    using System.Collections.Generic;

    using LionTrust.Foundation.SitecoreForms.Models;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Links;
    using Sitecore.Resources.Media;

    public static class ItemHelper
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
                return new Guid();
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
    }
}