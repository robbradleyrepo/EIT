using LionTrust.Foundation.Contact.Models;
using LionTrust.Foundation.Contact.Repositories;
using System;

namespace LionTrust.Foundation.Contact.Services
{
    public class PersonalizedContentService : IPersonalizedContentService
    {
        private readonly IPersonalizedContentPageRepository _personalizedContentPageRepository;
        private readonly IXConnectUtilityRepository _xconnectUtilityRepository;
        public PersonalizedContentService(IPersonalizedContentPageRepository personalizedContentPageRepository, IXConnectUtilityRepository xConnectUtilityRepository)
        {
            _personalizedContentPageRepository = personalizedContentPageRepository;
            _xconnectUtilityRepository = xConnectUtilityRepository;
        }
        public ScContactFacetData GetContactFacetData(string queryStringRef)
        {
            ScContactFacetData scContactFacetData = null;

            string sfEntityId = string.Empty, sfRandomGUID = string.Empty;

            //Query string "ref" should have the format as follows: {GUID}_{entityId}
            if (!string.IsNullOrEmpty(queryStringRef))
            {
                var queryStringParts = queryStringRef.Split('_');
                if (queryStringParts.Length >= 2)
                {
                    sfRandomGUID = queryStringParts[0];
                    sfEntityId = queryStringParts[1];
                }
            }

            var isSavingSFDataIntoScFacetSuccess = false;

            if (!string.IsNullOrEmpty(sfEntityId) && (sfEntityId.StartsWith("003", StringComparison.CurrentCultureIgnoreCase) || sfEntityId.StartsWith("00Q", StringComparison.CurrentCultureIgnoreCase)) && !string.IsNullOrEmpty(sfRandomGUID))
            {
                var isContact = (sfEntityId.StartsWith("003", StringComparison.CurrentCultureIgnoreCase)) ? true : false;
                //Identify current Sitecore contact and save relavant Salesforce data into the current Sitecore contact's S4SInfo facet
                isSavingSFDataIntoScFacetSuccess = _personalizedContentPageRepository.IdentifySitecoreContactAndSaveSFDataInFacet(sfEntityId, sfRandomGUID, isContact);
            }

            if (isSavingSFDataIntoScFacetSuccess)
            {
                //Get personalized content related to the current Sitecore contact
                scContactFacetData = _xconnectUtilityRepository.GetCurrentSitecoreContactFacetData();
            }

            return scContactFacetData;
        }
    }
}
