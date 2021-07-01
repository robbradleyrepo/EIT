namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using Sitecore.ContentSearch.ComputedFields;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.ContentSearch;
    using Sitecore.DependencyInjection;

    public class PageAuthorImageField : IComputedIndexField
    {
        private readonly IEnumerable<IPageAuthorImageField> fields;

        public PageAuthorImageField()
        {
            this.fields = ServiceLocator.ServiceProvider.GetServices<IPageAuthorImageField>();
        }

        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var indexItem = indexable as SitecoreIndexableItem;

            if (indexItem == null)
            {
                return null;
            }

            var item = indexItem.Item;

            var field = fields.FirstOrDefault(f => f.CanHandle(item.Template.BaseTemplates.Select(t => t.ID.Guid)));
            if (field == null)
            {
                return null;
            }

            return field.GetAuthorImageUrl(item);
        }
    }
}