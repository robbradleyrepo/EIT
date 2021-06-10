namespace LionTrust.Foundation.SitecoreForms.Factories
{
    using LionTrust.Foundation.SitecoreForms.Models;

    public interface IViewModelFactory
    {
        LinkViewModel BuildLink(IProperty<Link> link);
    }
}
