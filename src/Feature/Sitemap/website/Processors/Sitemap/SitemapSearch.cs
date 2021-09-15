namespace Sitecore.Feature.Sitemap.Processors.Sitemap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using LionTrust.Feature.Sitemap.Models;
    using LionTrust.Feature.Sitemap.Pipelines;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.ContentSearch.Utilities;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Globalization;

    public static class SitemapSearch
    {
        public static IQueryable<IndexedItem> GetAllItemsBySiteAndLanguage(Item homeItem, SiteDefinition def, Language language, IProviderSearchContext providerSearchContext)
        {
            var source =
              from i in providerSearchContext.GetQueryable<IndexedItem>()
              where i.Paths.Contains(homeItem.ID) && i.Language == language.Name && i.IncludeInSitemap
              select i;
            var first = PredicateBuilder.False<IndexedItem>();
            using (var enumerator = (
              from i in def.IncludedBaseTemplates
              select ID.Parse(i)).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var tmpl = enumerator.Current;
                    first = first.Or((IndexedItem i) => i.AllTemplates.Contains(IdHelper.NormalizeGuid(tmpl)));
                }
            }
            using (var enumerator = (
              from i in def.IncludedTemplates
              select ID.Parse(i)).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ID tmpl = enumerator.Current;
                    first = first.Or((IndexedItem i) => i.TemplateId == tmpl);
                }
            }
            Expression<Func<IndexedItem, bool>> expression = PredicateBuilder.True<IndexedItem>();
            using (IEnumerator<ID> enumerator = (
              from i in def.ExcludedItems
              select ID.Parse(i)).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ID id = enumerator.Current;
                    expression = expression.And((IndexedItem i) => i.ItemId != id);
                }
            }
            source = source.Where(first.And(expression));
            return source;
        }
    }
}