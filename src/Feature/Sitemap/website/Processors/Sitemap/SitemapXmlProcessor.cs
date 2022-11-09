namespace LionTrust.Feature.Sitemap.Processors.Sitemap
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Collections;
    using Sitecore.Configuration;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Security;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;
    using Sitecore.Globalization;
    using Sitecore.Links;
    using Sitecore.Sites;
    using LionTrust.Feature.Sitemap.Pipelines;
    using LionTrust.Feature.Sitemap.Processors.DefaultProcessor;
    using Sitecore;
    using Sitecore.Feature.Sitemap.Processors.Sitemap;

    public class SitemapXmlProcessor : CreateSitemapXmlProcessor
    {
        private string _indexName;

        public SitemapXmlProcessor(string indexName)
        {
            this._indexName = indexName;
        }

        private IEnumerable<UrlDefinition> ProcessSite(Item homeItem, SiteDefinition def, Language language)
        {
            IProviderSearchContext providerSearchContext;
            if (string.IsNullOrEmpty(this._indexName))
            {
                _indexName = def.IndexName;
            }

            providerSearchContext = ContentSearchManager.GetIndex(this._indexName).CreateSearchContext(SearchSecurityOptions.EnableSecurityCheck);

            try
            {
                var source = SitemapSearch.GetAllItemsBySiteAndLanguage(homeItem, def, language, providerSearchContext);
                var list = (from i in source
                            select
                                Factory.GetDatabase(i.DatabaseName)
                                    .GetItem(i.ItemId, Language.Parse(i.Language), Version.Latest)).ToList();

                var alreadyAdded = new HashSet<ID>();
                var urlOptions = GetUrlOptions(def, language);
                foreach (var current in list)
                {
                    if (current.Versions == null || current.Versions.Count == 0 || current.ID.IsNull || alreadyAdded.Contains(current?.ID))
                    {
                        continue;
                    }
                    alreadyAdded.Add(current.ID);

                    yield return new UrlDefinition(LinkManager.GetItemUrl(current, urlOptions), current.Statistics.Updated);//, alternateLocations);
                }
            }
            finally
            {
                providerSearchContext.Dispose();
            }
            yield break;
        }

        private UrlOptions GetUrlOptions(SiteDefinition def, Language language)
        {
            UrlOptions defaultOptions = UrlOptions.DefaultOptions;
            defaultOptions.SiteResolving = Settings.Rendering.SiteResolving;
            defaultOptions.Site = SiteContext.GetSite(def.SiteName);
            if (def.EmbedLanguage)
            {
                defaultOptions.LanguageEmbedding = LanguageEmbedding.Always;
            }
            else
            {
                defaultOptions.LanguageEmbedding = LanguageEmbedding.Never;
            }
            defaultOptions.AlwaysIncludeServerUrl = true;
            defaultOptions.Language = language;
            return defaultOptions;
        }

        private IEnumerable<Item> GetBlogCategoryItems(Item homeItem, Language language)
        {
            var blogCategories = Context.Database.GetItem(homeItem.Paths.FullPath + "/blog", language);
            return blogCategories != null ? blogCategories.Children : Enumerable.Empty<Item>();
        }

        public override void Process(CreateSitemapXmlArgs args)
        {
            LanguageCollection languages = LanguageManager.GetLanguages(Context.Database);
            Item item = Context.Database.GetItem(args.Site.RootPath + args.Site.StartItem);
            SiteDefinition siteDefinition = base.Configuration[args.Site.Name];

            if (siteDefinition.IndexName != _indexName)
            {
                return;
            }

            if (siteDefinition.EmbedLanguage)
            {
                foreach (Language current in languages.Where(f => f.Name != "en"))
                {
                    args.Nodes.AddRange(this.ProcessSite(item, siteDefinition, current));
                }
            }
            else
            {
                args.Nodes.AddRange(this.ProcessSite(item, siteDefinition, Context.Language));
            }
        }
    }
}
