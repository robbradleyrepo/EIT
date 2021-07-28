namespace LionTrust.Foundation.Contact.Repositories
{
    using LionTrust.Foundation.Contact.Models.Links;
    using LionTrust.Foundation.Contact.Models.Types;

    public interface IViewModelFactory
    {
        LinkViewModel BuildLink(IProperty<Link> link);
    }
}
