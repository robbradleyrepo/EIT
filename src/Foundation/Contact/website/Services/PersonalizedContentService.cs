namespace LionTrust.Foundation.Contact.Services
{
    using LionTrust.Foundation.Contact.Models;
    using LionTrust.Foundation.Contact.Repositories;
    using Sitecore.Web;
    using System;
    using System.Web;

    public class PersonalizedContentService : IPersonalizedContentService
    {
        private readonly IPersonalizedContentPageRepository _personalizedContentPageRepository;
        private readonly IXConnectUtilityRepository _xconnectUtilityRepository;
        public PersonalizedContentService(IPersonalizedContentPageRepository personalizedContentPageRepository, IXConnectUtilityRepository xConnectUtilityRepository)
        {
            _personalizedContentPageRepository = personalizedContentPageRepository;
            _xconnectUtilityRepository = xConnectUtilityRepository;
        }

        public ScContactFacetData GetContactFacetData()
        {
            ScContactFacetData scContactFacetData = null;

            var isSavingSFDataIntoScFacetSuccess = false;

            var context = GetContext();

            if (context != null)
            {
                //Identify current Sitecore contact and save relavant Salesforce data into the current Sitecore contact's S4SInfo facet
                isSavingSFDataIntoScFacetSuccess = _personalizedContentPageRepository.IdentifySitecoreContactAndSaveSFDataInFacet(context);
            }

            if (isSavingSFDataIntoScFacetSuccess)
            {
                //Get personalized content related to the current Sitecore contact
                scContactFacetData = _xconnectUtilityRepository.GetCurrentSitecoreContactFacetData();
            }

            return scContactFacetData;
        }

        public Context GetContext()
        {
            var context = (Context)WebUtil.GetSessionValue(Constants.SessionKeys.ContextSessionKey, HttpContext.Current);

            if (context == null)
            {
                var queryStringRef = WebUtil.GetQueryString(Constants.QueryStringNames.EmailPreferencefParams.RefQueryStringKey);

                //Query string "ref" should have the format as follows: {GUID}_{entityId}
                if (!string.IsNullOrEmpty(queryStringRef))
                {
                    var queryStringParts = queryStringRef.Split('_');
                    if (queryStringParts.Length >= 2)
                    {
                        var sfRandomGUID = queryStringParts[0];
                        var sfEntityId = queryStringParts[1];

                        context = CreateContext(sfEntityId, sfRandomGUID);
                    }
                }
                else
                {
                    var xconnectData = _xconnectUtilityRepository.GetCurrentSitecoreContactFacetData();

                    if (xconnectData != null)
                    {
                        context = CreateContext(xconnectData.SalesforceEntityId, xconnectData.RandomGuidFromSalesforceEntity);
                    }
                }
            }

            return context;
        }

        public void UpdateContext(Context context)
        {
            WebUtil.SetSessionValue(Constants.SessionKeys.ContextSessionKey, context);
        }

        private Context CreateContext(string sfEntityId, string sfRandomGUID)
        {
            if (string.IsNullOrEmpty(sfEntityId) || (!sfEntityId.StartsWith(Constants.PrefixSalesforceContact, StringComparison.CurrentCultureIgnoreCase) && !sfEntityId.StartsWith(Constants.PrefixSalesforceLead, StringComparison.CurrentCultureIgnoreCase)) || string.IsNullOrEmpty(sfRandomGUID))
            {
                return null;
            }

            var isContact = sfEntityId.StartsWith(Constants.PrefixSalesforceContact, StringComparison.CurrentCultureIgnoreCase) ? true : false;

            var sfEntityUtilityObj = new SFEntityUtility();

            var context = new Context
            {
                IsContact = isContact,
                SFRandomGUID = sfRandomGUID,
                SFEntityId = sfEntityId
            };

            context.Preferences = sfEntityUtilityObj.GetSFEmailPreferences(context, true);
            UpdateContext(context);

            return context;
        }
    }
}