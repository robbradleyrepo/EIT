namespace LionTrust.Feature.Navigation.Repositories
{
    using Sitecore.Data.Items;

    public interface INavigationRepository
    {
        Item GetSiteRootIdentity(Item contextItem);
        Item GetNavigationRoot(Item contextItem);
    }
}