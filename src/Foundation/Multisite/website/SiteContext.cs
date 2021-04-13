namespace LionTrust.Foundation.Multisite
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Multisite.Providers;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service]
    public class SiteContext
    {
        private readonly ISiteDefinitionsProvider siteDefinitionsProvider;

        public SiteContext(ISiteDefinitionsProvider siteDefinitionsProvider)
        {
            this.siteDefinitionsProvider = siteDefinitionsProvider;
        }

        public virtual SiteDefinition GetSiteDefinition(Item item)
        {
            Assert.ArgumentNotNull(item, nameof(item));

            return this.siteDefinitionsProvider.GetContextSiteDefinition(item);
        }
    }
}