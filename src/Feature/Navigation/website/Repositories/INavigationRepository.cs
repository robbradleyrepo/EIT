namespace LionTrust.Feature.Navigation.Repositories
{
    using Sitecore.Data.Items;

    public interface INavigationRepository
    {
        Item GetNavigationRoot(Item contextItem);
    }
}