using Glass.Mapper.Sc;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LionTrust.Foundation.SitecoreExtensions.Extensions
{
    public static class SitecoreServiceExtensions
    {
        public static IEnumerable<T> GetItems<T>(this ISitecoreService sitecoreService, string path, Guid templateId)
             where T : class
        {
            var query = new Query($"fast:{path}//*[@@templateid='{templateId}']");
            var items = sitecoreService.GetItems<T>(query);

            return items;
        }

        public static IEnumerable<T> GetChildren<T>(this ISitecoreService sitecoreService, Item item, Guid templateId)
             where T : class
        {
            var items = GetChildren(sitecoreService, item, templateId)
                            .Select(x => sitecoreService.GetItem<T>(x));

            return items;
        }

        public static IEnumerable<T> GetChildren<T>(this ISitecoreService sitecoreService, Guid itemId, Guid templateId)
             where T : class
        {
            var item = sitecoreService.GetItem<Item>(itemId);
            return GetChildren<T>(sitecoreService, item, templateId);
        }

        public static IEnumerable<Item> GetChildren(this ISitecoreService sitecoreService, Item item, Guid templateId)
        {
            var items = item?.Children.Where(x => x.TemplateID.Guid == templateId || x.Template.BaseTemplates.Any(bt => bt.ID.Guid == templateId));

            return items;
        }

        public static IEnumerable<Item> GetChildren(this ISitecoreService sitecoreService, Guid itemId, Guid templateId)
        {
            var item = sitecoreService.GetItem<Item>(itemId);
            var items = GetChildren(sitecoreService, item, templateId);

            return items;
        }
    }
}