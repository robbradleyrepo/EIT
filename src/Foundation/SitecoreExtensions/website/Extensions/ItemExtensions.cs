namespace LionTrust.Foundation.SitecoreExtensions.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;
    using Sitecore.Diagnostics;
    using Sitecore.Links;
    using Sitecore.Resources.Media;

    public static class ItemExtensions
    {  

        public static Item GetAncestorOrSelfOfTemplate(this Item item, ID templateID)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return item.DescendsFrom(templateID) ? item : item.Axes.GetAncestors().LastOrDefault(i => i.DescendsFrom(templateID));
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