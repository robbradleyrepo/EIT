namespace LionTrust.Foundation.Contact.Repositories
{
    using FuseIT.S4S.WebToSalesforce;
    using Sitecore.Diagnostics;
    using LionTrust.Foundation.Contact.Services;
    using LionTrust.Foundation.Contact.Models;

    /// <summary>
    /// Repository to contain functionalities related to read/write data using xconnect
    /// </summary>
    public class XConnectUtilityRepository : IXConnectUtilityRepository
    {
        private readonly ISitecoreContactUtility _sitecoreContactUtility;

        public XConnectUtilityRepository(ISitecoreContactUtility sitecoreContactUtility)
        {
            _sitecoreContactUtility = sitecoreContactUtility;
        }

        public ScContactFacetData GetCurrentSitecoreContactFacetData()
        {
            //Get curret visitor id
            WebToEntity webToSfEntity = new WebToEntity();
            var visitorId = webToSfEntity.GetCurrentVisitorId();

            if (!string.IsNullOrEmpty(visitorId))
            {
                return _sitecoreContactUtility.GetCurrentSitecoreContactFacetData(visitorId);
            }
            else
            {
                Log.Info("Sitecore visitor id is null or empty. No custom Salesforce data retrieved from current Sitecore contact facet.", this);
                return null;
            }
        }
    }
}
