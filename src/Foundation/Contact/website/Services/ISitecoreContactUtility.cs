namespace LionTrust.Foundation.Contact.Services
{
    using LionTrust.Foundation.Contact.Models;
    using Sitecore.XConnect;
    using System.Collections.Generic;

    public interface ISitecoreContactUtility
    {
        void SaveCustomSFDataIntoS4SInfoFacet(string scVisitorId, ScContactFacetData sfDataForSaving);

        void SaveSFFundIdsIntoS4SInforFacet(string scVisitorId, List<string> sfFundIds);

        ScContactFacetData GetCurrentSitecoreContactFacetData(string scVisitorId);

        string GetSalesforceEntityId(Contact contact);

        Enums.EntityType GetEntityType(string entityId);
    }
}
