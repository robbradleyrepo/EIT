namespace LionTrust.Foundation.Indexing.ComputedFields.SharedLogic
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using System.Collections.Generic;
    using System.Linq;

    public class SalesforceFundIdField : IComputedIndexField
    {
        private readonly IEnumerable<ISalesforceFundId> fields;

        public SalesforceFundIdField()
        {
            this.fields = Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetServices<ISalesforceFundId>();
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

            return field.GetSalesforceFundId(item); 
        }
    }
}