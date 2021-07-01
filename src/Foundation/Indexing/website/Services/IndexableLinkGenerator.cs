namespace LionTrust.Foundation.Indexing.Services
{
    using LionTrust.Foundation.DI;
    using Sitecore.Abstractions;
    using Sitecore.Data.Items;
    using Sitecore.Links;
    using Sitecore.Resources.Media;
    using Sitecore.Sites;

    [Service(ServiceType = typeof(IndexableLinkGenerator), Lifetime = Lifetime.Singleton)]
    public class IndexableLinkGenerator
    {
        private readonly BaseFactory _factory;
        private readonly BaseLinkManager _linkManager;

        public IndexableLinkGenerator(BaseFactory factory, BaseLinkManager linkManager)
        {
            this._factory = factory;
            this._linkManager = linkManager;
        }

        public string GenerateLink(Item item)
        {
            if (item == null)
            {
                return null;
            }
            using (new SiteContextSwitcher(_factory.GetSite(Constants.SiteName)))
            {
                if (item.Paths.IsMediaItem)
                {
                    var url = MediaManager.GetMediaUrl(item, new MediaUrlOptions { AlwaysIncludeServerUrl = false });
                    return HashingUtils.ProtectAssetUrl(url);
                }
                else
                {
                    return _linkManager.GetItemUrl(item, new UrlOptions { AlwaysIncludeServerUrl = false, LanguageEmbedding = LanguageEmbedding.Never });
                }
            }
        }
    }
}