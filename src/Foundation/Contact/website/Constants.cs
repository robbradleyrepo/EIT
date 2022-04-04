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

        public const string SFProductEntityName = "Product__c";
        public const string SFFundPreferenceEntityName = "Fund_Preference__c";
        public const string SFContactEntityName = "Contact";
        public const string SfLeadEntityName = "Lead";

        public const string SF_EmailOptOutFacetKey = "HasOptedOutOfEmail";

        //Salesforce record types for Product
        public const string SFProcessRecordTypeName = "Process";
        public const string SFFundRecordTypeName = "Fund";

        //Salesfore Product Status field values
        public const string SFProductStatusOpenName = "Open";

        //Salesforce common for Product
        public const string SFProduct_StatusField = "Status__c";
        public const string SFProduct_IsMutiAssetScenarioField = "Is_Multi_Asset_Scenario__c";
        public const string SFProduct_IsNonUKDomicile = "Non_UK_Domicile__c";

        //Salesforce FundPreference field names
        public const string SFFundPref_IdField = "Id";
        public const string SFFundPref_ContactField = "Contact__c";
        public const string SFFundPref_LeadField = "Lead__c";
        public const string SFFundPref_FundField = "Fund__c";
        public const string SFFundPref_InterestedField = "Interested__c";
        public const string SFFundPref_RefFieldForProcessId = "Fund__r.Strategy__c";
        public const string SFFundPref_FundStatusField = "Fund__r.Status__c";
        public const string SFFundPref_ProcessStatusField = "Fund__r.Strategy__r.Status__c";

        //Salesforce Process field names
        public const string SFProcess_RefFieldForFunds = "Products__r";

        //Salesforce Contact and Lead field names
        public const string SF_LTNewsField = "Liontrust_News__c";
        public const string SF_FactSheetField = "Factsheets__c";
        public const string SF_RelBlogField = "Relevant_Blogs__c";
        public const string SF_Commentaries = "Monthly_Commentaries__c";
        public const string SF_EmailOptOutField = "HasOptedOutOfEmail";
        public const string SF_DateOfConcentField = "Date_of_Consent_Given__c";
        public const string SF_SitecoreVistorIdField = "FuseITAnalytics__SitecoreVisitorId__c";
        public const string SF_GUIDForEmailPref = "GUID_For_EmailPreferences__c";
        public const string SF_FirstNameField = "FirstName";
        public const string SF_LastNameField = "LastName";
        public const string SF_EmailField = "Email";
        public const string SF_UKResident = "UK_Resident__c";
        public const string SF_RecordTypeField = "RecordTypeId";
        public const string SFContact_CountryField = "ContactCountry__c";
        public const string SFContact_CompanyFCAIdField = "Company_FCA_Number__c";
        public const string SFContact_CompanyNameField = "Company_Nam_e__c";
        public const string SFContact_OrgNameField = "AccountId";
        public const string SFContact_InstitutionalBulletin = "Institutional_Bulletin__c";
        public const string SFLead_CountryField = "Country__c";
        public const string SFLead_CompanyField = "Company";
        public const string SF_Owner_NameField = "Pardot_Contact_Owner_Pardot_Name__c";
        public const string SF_Owner_TitleField = "Pardot_Contact_Owner_Pardot_Title__c";
        public const string SF_Owner_EmailField = "Pardot_Contact_Owner_Pardot_Email__c";
        public const string SF_Owner_PhoneField = "Pardot_Contact_Owner_Pardot_Phone__c";
        public const string SF_Owner_RegionField = "Pardot_Contact_Owner_Pardot_Region__c";
        public const string SF_Hard_Bounced = "pi__pardot_hard_bounced__c";

        //S4S SitecoreXDBContact Salesforce object fields
        public const string SFSitecoreXDBContactObjectName = "FuseITAnalytics__SitecoreXDBContact__c";
        public const string SFSitecoreXDBContactObject_SitecoreAliasIdField = "FuseITAnalytics__AliasId__c";
        public const string SFSitecoreXDBContactObject_ContactField = "FuseITAnalytics__Contact__c";
        public const string SFSitecoreXDBContactObject_LeadField = "FuseITAnalytics__Lead__c";

        //Salesforce Lead record type values
        public const string SFLeadRecordType_Subscribe = "Subscribed Lead";
        public const string SFLeadRecordType_UnSubscribe = "Unsubscribed Lead";

        //Salesforce Contact record type values
        public const string SFContactRecortType_SitecoreContact = "Sitecore Contact";
        public const string SFContactRecordType_UnSubscribe = "Unsubscribed Contact";

        public const string IdentifierSourceConfigName = "S4S.Analytics.IdentifierSource";

        public static class QueryStringNames
        {
            public static class EmailPreferencefParams
            {
                public static readonly string RefQueryStringKey = "ref";
                public static readonly string EmailQueryStringKey = "email";
                public static readonly string IsContactQueryStringKey = "iscontact";
                public static readonly string ErrorQueryStringKey = "error";
                public static readonly string DatasourceIdQueryStringKey = "dataSourceId";
            }
        }

        public static class SessionKeys
        {
            public const string ContextSessionKey = "context";
        }
    }
}