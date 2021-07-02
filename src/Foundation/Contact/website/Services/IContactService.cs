namespace LionTrust.Foundation.Contact.Services
{
    using LionTrust.Foundation.Contact.Models;
    using System.Collections.Generic;

    public interface IContactService
    {
        void SaveCustomSFDataIntoS4SInfoFacet(string scVisitorId, ScContactFacetData sfDataForSaving);

        void SaveSFFundIdsIntoS4SInforFacet(string scVisitorId, List<string> sfFundIds);

        ScContactFacetData GetCurrentSitecoreContactFacetData(string scVisitorId);
    }
}
