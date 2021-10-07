using LionTrust.Foundation.Contact.Models;

namespace LionTrust.Foundation.Contact.Repositories
{
    public interface IPersonalizedContentPageRepository
    {
        bool IdentifySitecoreContactAndSaveSFDataInFacet(Context context);
    }
}
