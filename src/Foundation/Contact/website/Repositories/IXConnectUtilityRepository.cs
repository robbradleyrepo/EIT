namespace LionTrust.Foundation.Contact.Repositories
{
    using LionTrust.Foundation.Contact.Models;

    public interface IXConnectUtilityRepository
    {        
        ScContactFacetData GetCurrentSitecoreContactFacetData();
    }
}
