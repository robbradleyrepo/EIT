namespace LionTrust.Foundation.Contact.Extensions
{
    using System;
    using System.Linq;
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

    public static class FieldExtensions
    {
        public static string SetMediaItem(this Field field, Guid mediaId)
        {
            var imageField = new ImageField(field);

            imageField.MediaID = new ID(mediaId);
            imageField.SetAttribute("showineditor", "1");

            return imageField.Value;
        }

        public static string SetMultilistFieldValue(this Field field, string fieldValue, Item container, string fieldName)
        {
            var multilistField = new MultilistField(field);

            var item =
                container.Axes.GetDescendants()
                    .FirstOrDefault(i => i[fieldName].Equals(fieldValue, StringComparison.InvariantCultureIgnoreCase));

            if (item != null)
            {
                var itemGuid = MainUtil.GuidToString(item.ID.Guid);

                if (!multilistField.Contains(itemGuid))
                {
                    multilistField.Add(itemGuid);
                }
            }

            return multilistField.Value;
        }
    }
}
