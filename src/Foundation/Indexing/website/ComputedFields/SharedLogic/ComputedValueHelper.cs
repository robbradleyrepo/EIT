namespace LionTrust.Foundation.Indexing.ComputedFields.SharedLogic
{
    using System;
    using System.Collections.Generic;

    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Data.Fields;
    using Sitecore.Globalization;
    using System.Linq;
    using Sitecore.ContentSearch;

    public static class ComputedValueHelper
    {
        public static Item CheckCastComputedFieldItem(IIndexable indexable)
        {
            if (indexable == null)
            {
                throw new ArgumentNullException("indexable");
            }

            var scIndexable = indexable as SitecoreIndexableItem;

            if (scIndexable == null)
            {
                return scIndexable;
            }

            var item = (Item)scIndexable;

            if (string.Compare(item.Database.Name, "core", StringComparison.OrdinalIgnoreCase) == 0)
            {
                item = null;
            }

            return item;
        }

        public static List<Guid> ExtractIds(Field multiValueField)
        {
            var referencedItemGuids = multiValueField.Value.Split('|');
            var ids = new List<Guid>();

            foreach (var referencedItemGuid in referencedItemGuids)
            {
                if (!string.IsNullOrEmpty(referencedItemGuid))
                {
                    if (Guid.TryParse(referencedItemGuid, out Guid itemId) && itemId != Guid.Empty)
                    {
                        ids.Add(itemId);
                    }
                }
            }

            return ids;
        }

        public static string GetMultiListValue(Field multiValueField, params string[] textFieldNames)
        {
            var ids = ExtractIds(multiValueField);
            return GetMultiListValue(multiValueField.Database, multiValueField.Language, ids, textFieldNames);
        }

        public static string GetMultiListValue(Database database, Language language, IEnumerable<Guid> referencedItemGuids, params string[] textFieldNames)
        {
            var referencedItemFieldValues = new List<string>();
            foreach (var referencedItemGuid in referencedItemGuids)
            {
                // Intentionally choosing not to use Glass to get the item
                Item item = database.GetItem(new ID(referencedItemGuid), language);
                if (item != null)
                {
                    var value = GetFieldValue(item, textFieldNames);
                    referencedItemFieldValues.Add(value);
                }
            }

            if (!referencedItemFieldValues.Any())
            {
                return null;
            }

            return string.Join("|", referencedItemFieldValues);
        }

        private static string GetFieldValue(Item item, params string[] textFieldNames)
        {
            if (textFieldNames == null)
            {
                return item.Name;
            }

            var fields = string.Empty;
            var index = 0;
            foreach (var fieldName in textFieldNames)
            {
                var value = item[fieldName];
                if (!string.IsNullOrEmpty(value))
                {
                    if (index == 0)
                    {
                        fields = value;
                    }
                    else
                    {
                        fields = $"{fields},{value}";
                    }
                }

                index++;
            }

            return fields;
        } 
    }
}