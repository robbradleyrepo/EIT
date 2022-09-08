using FuseIT.Sitecore.Personalization.Facets;
using LionTrust.Feature.EXM.Services.Interfaces;
using Sitecore.Data;
using Sitecore.EmailCampaign.XConnect.Web;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Modules.EmailCampaign.Core.Contacts;
using Sitecore.XConnect;
using System.Linq;

namespace LionTrust.Feature.EXM.Services.Implementations
{
    public class CustomContactService : ContactService, ICustomContactService
    {
        public CustomContactService(IXConnectClientFactory xConnectClientFactory, XConnectRetry xConnectRetry, ILogger logger)
            : base(xConnectClientFactory, xConnectRetry, logger) { }

        public new Contact GetContact(ID contactId, params string[] facetKeys)
        {
            facetKeys = facetKeys.Concat(new[] { S4SInfo.DefaultFacetKey }).ToArray();
            return base.GetContact(contactId, facetKeys);
        }

        public new Contact GetContact(ContactIdentifier contactIdentifier, params string[] facetKeys)
        {
            facetKeys = facetKeys.Concat(new[] { S4SInfo.DefaultFacetKey }).ToArray();
            return base.GetContact(contactIdentifier, facetKeys);
        }
    }
}