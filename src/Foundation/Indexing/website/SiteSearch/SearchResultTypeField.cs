namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using System.Collections.Generic;
    using System.Linq;

    public class SearchResultTypeField : IComputedIndexField
    {
        private readonly IEnumerable<ISearchResultType> fields;

        public SearchResultTypeField()
        {
            this.fields = Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetServices<ISearchResultType>();
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
                return "Page";
            }

            return field.GetSearchResultType();
        }
    }
}