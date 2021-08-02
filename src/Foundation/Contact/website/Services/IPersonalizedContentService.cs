namespace LionTrust.Foundation.Contact.Services
{
    using LionTrust.Foundation.Contact.Models;

    public interface IPersonalizedContentService
    {
        ScContactFacetData GetContactFacetData(string queryStringRef);
    }
}