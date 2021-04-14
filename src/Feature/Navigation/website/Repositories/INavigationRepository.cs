namespace LionTrust.Feature.Navigation.Repositories
{
    using Sitecore.Data.Items;

    public interface INavigationRepository
    {
        Item GetNavigationSiteRoot(Item contextItem);
        Item GetNavigationRoot(Item contextItem);
    }
}