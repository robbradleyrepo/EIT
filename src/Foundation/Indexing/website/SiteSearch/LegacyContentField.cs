namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LegacyContentField : IComputedIndexField
    {
        private readonly IEnumerable<ILegacyContentField> fields;

        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public LegacyContentField()
        {
            this.fields = Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetServices<ILegacyContentField>();
        }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var indexItem = indexable as SitecoreIndexableItem;

            if (indexItem == null)
            {
                return null;
            }

            var item = indexItem.Item;
            var templates = new List<Guid>();
            GetAllTemplates(item.Template, templates);
            var field = fields.FirstOrDefault(f => f.CanHandle(templates));
            if (field == null)
            {
                return null;
            }

            return field.GetData(item);
        }

        private void GetAllTemplates(TemplateItem baseTemplate, IList<Guid> templates)
        {
            if (baseTemplate == null || baseTemplate.ID == Sitecore.TemplateIDs.StandardTemplate)
            {
                return;
            }
            
            templates.Add(baseTemplate.ID.Guid);

            foreach (var item in baseTemplate.BaseTemplates)
            {
                GetAllTemplates(item, templates);
            }
        }
    }
}