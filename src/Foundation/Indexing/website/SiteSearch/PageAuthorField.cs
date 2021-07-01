namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using System.Collections.Generic;
    using System.Linq;

    public class PageAuthorField : IComputedIndexField
    {
        private readonly IEnumerable<IPageAuthorField> fields;

        public PageAuthorField()
        {
            this.fields = Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetServices<IPageAuthorField>(); ;            
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

            return field.GetAuthorName(item);
        }
    }
}