namespace LionTrust.Foundation.Contact.Repositories
{
    using LionTrust.Foundation.Contact.Models.Types;
    using LionTrust.Foundation.Contact.Extensions;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Codehouse.Common;

    class EntityFactory : IEntityFactory
    {
        public T Build<T>(Item item)
        {
            return Build<T>(item, null);
        }

        public T Build<T>(Item item, IDictionary<string, string> renderingParameters)
        {
            var obj = Activator.CreateInstance(typeof(T));
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;

                SetPropertyValue(propertyType, obj, property, item, renderingParameters);
            }

            return (T)obj;
        }

        private void SetPropertyValue(Type type, object obj, PropertyInfo property, Item item, IDictionary<string, string> renderingParameters)
        {
            var @switch = GetSwitch(obj, property, item, renderingParameters);

            if (@switch.ContainsKey(type))
            {
                @switch[type]();
            }
        }


        private IDictionary<Type, Action> GetSwitch(object obj, PropertyInfo property, Item item, IDictionary<string, string> renderingParameters)
        {
            return new Dictionary<Type, Action>
            {
                {
                    typeof (IProperty<Image>), () => property.SetValue(obj, item.GetImage(property.Name), null)
                },
                {
                    typeof (IProperty<Link>), () => property.SetValue(obj, item.GetLink(property.Name, renderingParameters), null)
                },
                {
                    typeof (IProperty<string>), () => property.SetValue(obj, item.GetIPropertyString(property.Name), null)
                },
                {
                    typeof (int), () => property.SetValue(obj, item.GetNumberFieldValue(property.Name), null)
                },
                {
                    typeof (DateTime), () => property.SetValue(obj, item.GetDateFieldValue(property.Name), null)
                },
                {
                    typeof (string), () => property.SetValue(obj, item[property.Name], null)
                },
                 {
                    typeof (bool), () => property.SetValue(obj, item.GetBooleanFieldValue(property.Name), null)
                },
                {
                    typeof (ItemProperty), () => property.SetValue(obj, item.GetItemProperty(), null)
                },
                {
                    typeof (DropLink), () => property.SetValue(obj, item.GetDropLinkItem(property.Name), null)
                },
                {
                    typeof (Guid), () => property.SetValue(obj, item.GetGuid(property.Name), null)
                }
            };

        }
    }
}
