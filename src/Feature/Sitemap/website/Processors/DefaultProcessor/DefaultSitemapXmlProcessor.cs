namespace LionTrust.Feature.Sitemap.Processors.DefaultProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Sitecore.Collections;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.ContentSearch.Security;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;
    using Sitecore.Globalization;
    using Sitecore.Links;
    using Sitecore.Sites;
    using LionTrust.Feature.Sitemap.Pipelines;
    using Sitecore;

    public class DefaultSitemapXmlProcessor : CreateSitemapXmlProcessor
    {
        private string indexName;

        public DefaultSitemapXmlProcessor(string indexName)
        {
            this.indexName = indexName;
        }

        private IEnumerable<UrlDefinition> ProcessSite(Item homeItem, SiteDefinition def, Language language)
        {
            using (IProviderSearchContext providerSearchContext = !string.IsNullOrEmpty(this.indexName) ? ContentSearchManager.GetIndex(this.indexName).CreateSearchContext(SearchSecurityOptions.EnableSecurityCheck) : ContentSearchManager.GetIndex((IIndexable)(SitecoreIndexableItem)homeItem).CreateSearchContext(SearchSecurityOptions.EnableSecurityCheck))
            {
                IQueryable<SitemapResultItem> results = providerSearchContext.GetQueryable<SitemapResultItem>().Where<SitemapResultItem>((Expression<Func<SitemapResultItem, bool>>)(i => i.Paths.Contains<ID>(homeItem.ID) && i.Language == language.Name));
                Expression<Func<SitemapResultItem, bool>> tmplPred = PredicateBuilder.False<SitemapResultItem>();
                foreach (ID id in def.IncludedBaseTemplates.Select<string, ID>((Func<string, ID>)(i => ID.Parse(i))))
                {
                    ID tmpl = id;
                    tmplPred = tmplPred.Or<SitemapResultItem>((Expression<Func<SitemapResultItem, bool>>)(i => i.AllTemplates.Contains<ID>(tmpl)));
                }
                foreach (ID id in def.IncludedTemplates.Select<string, ID>((Func<string, ID>)(i => ID.Parse(i))))
                {
                    ID tmpl = id;
                    tmplPred = tmplPred.Or<SitemapResultItem>((Expression<Func<SitemapResultItem, bool>>)(i => i.TemplateId == tmpl));
                }
                Expression<Func<SitemapResultItem, bool>> itemPred = PredicateBuilder.True<SitemapResultItem>();
                foreach (ID id1 in def.ExcludedItems.Select<string, ID>((Func<string, ID>)(i => ID.Parse(i))))
                {
                    ID id = id1;
                    itemPred = itemPred.And<SitemapResultItem>((Expression<Func<SitemapResultItem, bool>>)(i => i.ItemId != id));
                }
                results = results.Where<SitemapResultItem>(tmplPred.And<SitemapResultItem>(itemPred));
                List<Item> items = results.Select<SitemapResultItem, Item>((Expression<Func<SitemapResultItem, Item>>)(i => Factory.GetDatabase(i.DatabaseName).GetItem(i.ItemId, Language.Parse(i.Language), Sitecore.Data.Version.Latest))).ToList<Item>();
                StringBuilder sb = new StringBuilder();
                UrlOptions options = UrlOptions.DefaultOptions;
                options.SiteResolving = Settings.Rendering.SiteResolving;
                options.Site = SiteContext.GetSite(def.SiteName);
                options.LanguageEmbedding = !def.EmbedLanguage ? LanguageEmbedding.Never : LanguageEmbedding.Always;
                options.AlwaysIncludeServerUrl = true;
                options.Language = language;
                foreach (Item obj in items)
                {
                    if (obj.Versions.Count > 0)
                    {
                        yield return new UrlDefinition(LinkManager.GetItemUrl(obj, options), obj.Statistics.Updated);
                    }
                }
            }
        }

        public override void Process(CreateSitemapXmlArgs args)
        {
            LanguageCollection languages = LanguageManager.GetLanguages(Context.Database);
            Item homeItem = Context.Database.GetItem(args.Site.RootPath + args.Site.StartItem);
            SiteDefinition def = this.Configuration[args.Site.Name];
            if (def.EmbedLanguage)
            {
                foreach (Language language in (Collection<Language>)languages)
                    args.Nodes.AddRange(this.ProcessSite(homeItem, def, language));
            }
            else
                args.Nodes.AddRange(this.ProcessSite(homeItem, def, Context.Language));
        }
    }
}