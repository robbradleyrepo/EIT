namespace LionTrust.Foundation.Contact.Repositories
{
    using System;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using System.Collections.Generic;
    using System.Linq;
    using Codehouse.Common;

    public class BaseRepository : IBaseRepository
    {
        protected readonly IEntityFactory EntityFactory;

        public Guid FolderId { get; set; }
        public Guid TemplateId { get; set; }

        private const string SingleValueQuery = "{0}/*[@@templateid='{1}' and @{2} = '{3}']";
        private const string MultiValueQuery = "{0}/*[@@templateid='{1}' and contains(@{2},'{3}')]";
        public BaseRepository(IEntityFactory entityFactory)
        {
            EntityFactory = entityFactory;
        }

        protected Database Database
        {
            get
            {
                if (Sitecore.Context.Database != null)
                {
                    return Sitecore.Context.Database;
                }
                else
                {
                    return Sitecore.Configuration.Factory.GetDatabase("web");
                }
            }
        }

        protected Item GetItem(Guid id)
        {
            if (Sitecore.Context.Item != null && Sitecore.Context.Item.ID.Guid.Equals(id))
            {
                return Sitecore.Context.Item;
            }
            else
            {
                return Database.GetItem(new ID(id));
            }
        }

        protected string GetFieldValue(Guid id, string fieldName)
        {
            var fieldValue = string.Empty;

            var item = Database.GetItem(new ID(id));
            if (item != null)
            {
                fieldValue = item[fieldName];
            }

            return fieldValue;
        }

        protected T GetPageSection<T>()
        {
            var result = EntityFactory.Build<T>(Sitecore.Context.Item);

            return result;
        }

        public virtual IList<T> GetObjectsByQuery<T>(string fieldName, Guid fieldValueId, bool isTheMultiValueField = false) where T : class
        {
            var tList = new List<T>();

            var tItems = GetItemsByQuery<T>(fieldName, fieldValueId, isTheMultiValueField);

            if (tItems != null && tItems.Any())
            {
                tItems.ForEach(r =>
                {
                    tList.Add(LoadChildren<T>(r, EntityFactory.Build<T>(r)));
                });
            }

            return tList;
        }

        public virtual Item[] GetItemsByQuery<T>(string fieldName, Guid fieldValueId,
            bool isTheMultiValueField = false) where T : class
        {
            var folderItem = GetItem(FolderId);
            if (folderItem == null)
            {
                return new Item[0];
            }

            var query = isTheMultiValueField ? MakeMultiValueQuery(folderItem, fieldName, fieldValueId) : MakeSingleValueQuery(folderItem, fieldName, fieldValueId);

            return Database.SelectItems(query);
        }

        public virtual T LoadChildren<T>(Item item, T obj) where T : class
        {
            return obj;
        }

        private string MakeSingleValueQuery(Item item, string fieldName, Guid fieldValueId)
        {
            return SingleValueQuery.FormatWith(item.Paths.LongID, TemplateId.ToString("B").ToUpper(),
                    fieldName, fieldValueId.ToString("B").ToUpper());
        }

        private string MakeMultiValueQuery(Item item, string fieldName, Guid fieldValueId)
        {
            return MultiValueQuery.FormatWith(item.Paths.LongID, TemplateId.ToString("B").ToUpper(),
                    fieldName, fieldValueId.ToString("B").ToUpper());
        }
    }
}