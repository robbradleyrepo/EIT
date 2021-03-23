namespace LionTrust.Feature.Navigation.Repositories
{
    using System;
    using System.Linq;
    using LionTrust.Foundation.SitecoreExtensions.Extensions;
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Sites;

    public class NavigationRepository : INavigationRepository
    {
        public Item ContextItem { get; }
        public Item NavigationRoot { get; }
        public Item SiteRootIdentity { get; }


        public NavigationRepository(Item contextItem)
        {
            this.ContextItem = contextItem;
            this.SiteRootIdentity = this.GetSiteRootIdentity(this.ContextItem);
            this.NavigationRoot = this.GetNavigationRoot(this.ContextItem);
            if (this.NavigationRoot == null)
            {
                throw new InvalidOperationException($"Cannot determine navigation root from '{this.ContextItem.Paths.FullPath}'");
            }
        }

        public Item GetSiteRootIdentity(Item contextItem)
        {
            return contextItem.GetAncestorOrSelfOfTemplate(Navigation.Constants.SiteRoot.TemplateID) ?? Context.Site.GetContextItem(Navigation.Constants.SiteRoot.TemplateID);
        }

        public Item GetNavigationRoot(Item contextItem)
        {
            return contextItem.GetAncestorOrSelfOfTemplate(Navigation.Constants.Home.TemplateID) ?? Context.Site.GetContextItem(Navigation.Constants.Home.TemplateID);
        }
    }
}