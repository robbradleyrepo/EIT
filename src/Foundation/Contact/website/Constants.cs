using System;

namespace LionTrust.Foundation.Contact
{
    public static class Constants
    {
        //Facet key name to store Salesforce fund ids in S4SInfo.Fields facet
        public const string SFFundIdFacetKey = "SalesforceFundIds";
        public const string SFRandomGuidFacetKey = "RandomGuid";
        public const string SFLeadIdFacetKey = "S4SSalesforceLeadId";
        public const string SFContactIdFacetKey = "S4SSalesforceContactId";
        public const string SFOrgIdFacetKey = "S4SSalesforceOrgId";
        public const string SFFirstNameFacetKey = "FirstName";
        public const string SFLastNameFacetKey = "LastName";
        public const string PrefixSalesforceContact = "003";
        public const string PrefixSalesforceLead = "00Q";
        
    }
}