namespace LionTrust.Feature.Search.SiteSearch
{
    using LionTrust.Foundation.Indexing.SiteSearch;
    using Sitecore.ContentSearch;
    using LionTrust.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using Sitecore.Data.Fields;
    using Sitecore;
    using System.Text;
    using System.Linq;
    using LionTrust.Foundation.DI;

    [Service(ServiceType = typeof(ILegacyContentField), Lifetime = Lifetime.Singleton)]
    public class LegacyArticleContentField : ILegacyContentField
    {        
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(Foundation.Legacy.Constants.PresentationBase.TemplateId);
        }

        public string GetData(Item item)
        {            
            if (item == null)
            {
                return null;
            }

            if (!this.ShouldIndexItem(item))
            {
                return null;
            }            
            var result = new StringBuilder();

            item.Fields.ReadAll();
            foreach (var field in item.Fields.Where(this.ShouldIndexField))
            {
                result.AppendLine(field.Value);
            }

            return result.ToString();
        }

        private bool ShouldIndexItem(Item item)
        {
            return item.HasLayout() && !item.Paths.LongID.Contains(ItemIDs.TemplateRoot.ToString());
        }

        private bool ShouldIndexField(Field field)
        {
            return !field.Name.StartsWith("__") && this.IsTextField(field) && !string.IsNullOrEmpty(field.Value);
        }

        private bool IsTextField(Field field)
        {
            return IndexOperationsHelper.IsTextField((SitecoreItemDataField)field);
        }
    }
}