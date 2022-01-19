namespace LionTrust.Feature.Navigation.Repositories
{
    using System;

    using LionTrust.Foundation.SitecoreExtensions.Extensions;
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Items;

    public class NavigationRepository : INavigationRepository
    {
        public Item ContextItem { get; }
        public Item NavigationRoot { get; }

        public NavigationRepository(Item contextItem)
        {
            this.ContextItem = contextItem;
            this.NavigationRoot = this.GetNavigationRoot(this.ContextItem);
            if (this.NavigationRoot == null)
            {
                throw new InvalidOperationException($"Cannot determine navigation root from '{this.ContextItem.Paths.FullPath}'");
            }
        }

        public Item GetNavigationRoot(Item contextItem)
        {
            return contextItem.GetAncestorOrSelfOfTemplate(new ID(Navigation.Constants.NavigationRoot.TemplateID)) ?? Context.Site.GetContextItem(new ID(Navigation.Constants.NavigationRoot.TemplateID));
        }
    }
}