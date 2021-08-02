namespace LionTrust.Foundation.Contact.Repositories
{
    public interface IPersonalizedContentPageRepository
    {
        bool IdentifySitecoreContactAndSaveSFDataInFacet(string sfEntityId, string sfRandomGUID, bool isContact);
    }
}
