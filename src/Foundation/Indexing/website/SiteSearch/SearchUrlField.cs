namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using LionTrust.Foundation.Indexing.ComputedFields.SharedLogic;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Links;
    using Sitecore.Sites;
    using System.Collections.Generic;
    using System.Linq;

    public class SearchUrlField : IComputedIndexField
    {
        private readonly IEnumerable<ISearchUrlField> fields;

        public SearchUrlField()
        {
            this.fields = Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetServices<ISearchUrlField>();
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

            var item = ComputedValueHelper.CheckCastComputedFieldItem(indexable);
            if (item == null)
            {
                return null;
            }

            var field = fields.FirstOrDefault(f => f.CanHandle(item.Template.BaseTemplates.Select(t => t.ID.Guid)));

            if (field == null)
            {
                using (new SiteContextSwitcher(Factory.GetSite(Constants.SiteName)))
                {
                    return LinkManager.GetItemUrl(item, new UrlOptions { AlwaysIncludeServerUrl = true, LowercaseUrls = true });
                }
            }

            return field.GetUrl(item);
        }
    }
}