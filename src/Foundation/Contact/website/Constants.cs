using System;

namespace LionTrust.Foundation.Contact
{
    public static class Constants
    {
        public const string SFProductEntityName = "Product__c";
        public const string SFFundPreferenceEntityName = "Fund_Preference__c";
        public const string SFContactEntityName = "Contact";
        public const string SfLeadEntityName = "Lead";

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
        public const string SF_Article41NoticeSentField = "Article_14_Notice_Sent__c";
        public const string SF_RecordTypeField = "RecordTypeId";
        public const string SFContact_CountryField = "ContactCountry__c";
        public const string SFContact_CompanyFCAIdField = "Company_FCA_Number__c";
        public const string SFContact_CompanyNameField = "Company_Nam_e__c";
        public const string SFContact_OrgNameField = "AccountId";
        public const string SFContact_InstitutionalBulletin = "Institutional_Bulletin__c";
        public const string SFLead_CountryField = "Country__c";
        public const string SFLead_CompanyField = "Company";

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

        //Facet key name to store Salesforce fund ids in S4SInfo.Fields facet
        public const string SFFundIdFacetKey = "SalesforceFundIds";
        public const string SFFirstNameFacetKey = "FirstName";
        public const string SFLastNameFacetKey = "LastName";
        public const string SFRandomGuidFacetKey = "RandomGuid";
        public const string SFLeadIdFacetKey = "S4SSalesforceLeadId";
        public const string SFContactIdFacetKey = "S4SSalesforceContactId";
        public const string SFOrgIdFacetKey = "S4SSalesforceOrgId";

        public const string PrefixSalesforceContact = "003";
        public const string PrefixSalesforceLead = "00Q";

        public static class QueryStringNames
        {
            public static class Shared
            {
                public static readonly string PageNumber = "page";

                public static class Fund
                {
                    public static readonly string Class = "class";
                    public static readonly string Objective = "objective";
                    public static readonly string Process = "process";
                    public static readonly string Region = "region";
                    public static readonly string Type = "type";
                    public static readonly string Ids = "fundid";
                    public static readonly string Year = "documentyear";

                    public static class Documents
                    {
                        public static readonly string Ids = "itemids";
                        public static readonly string Types = "documenttypes";
                        public static readonly string All = "all";
                        public static readonly string Template = "template";

                        public static readonly string AllDocumentTypes = "All Document Types";
                        public static readonly string AllFunds = "All Funds";
                        public static readonly string AllFundManagers = "All Fund Managers";


                    }

                }
            }

            public static class SearchResultsPage
            {
                public static readonly string SiteSearchQuery = "q";
                public static readonly string TagResultQuery = "tag";
                public static readonly string SearchCategoriesQuery = "category";

                public static class SearchCategoryValues
                {
                    public static readonly string WebsiteContent = "webcontent";
                    public static readonly string BlogContent = "blogcontent";
                    public static readonly string DocumentContent = "documents";
                }
            }

            public static class ArticleListingPage
            {
                public static readonly string TypeQuery = "type";
                public static readonly string AuthorQuery = "author";
                public static readonly string BlogTypeQuery = "blogtype";
                public static readonly string FundQuery = "fund";
                public static readonly string YearQuery = "year";
                public static readonly string MonthQuery = "month";
                public static readonly string BlogSearchTermQuery = "search";
            }

            public static class ListingWithFiltersModulesParams
            {
                public static readonly string YearQuery = "year";
                public static readonly string MonthQuery = "month";
                public static readonly string ListingSearchTermQuery = "search";
                public static readonly string ListingType = "type";
            }

            public static class QueryStringKeyValues
            {
                public static readonly string DocumentNameSortAZ = "AZ";
                public static readonly string DocumentNameSortZA = "ZA";
                public static readonly string DocumentCustomSortOrder = "CustomSort";
            }

            public static class EmailPreferencefParams
            {
                public static readonly string RefQueryStringKey = "ref";
                public static readonly string EmailQueryStringKey = "email";
                public static readonly string IsContactQueryStringKey = "iscontact";
            }

            public static class PersonalizedFundsAndArticlesComponentParams
            {
                public static readonly string FundQuery = "fund";
                public static readonly string RefQueryStringKey = "ref";
            }
        }

        public static class ItemIds
        {
            public static class Content
            {
                public static class Page
                {
                    public static readonly Guid SearchResult = new Guid("{38DFA823-6AE3-47FA-8C01-4C115BC39BD1}");
                    public static readonly Guid BlogPage = new Guid("{32E0FB65-AE9B-4806-B94F-C015BD3100F5}");
                }

                public static class Global
                {
                    public static class BlogType
                    {
                        public static readonly Guid BlogPost = new Guid("{A0938A5B-ECE4-47D8-8564-421C0D816141}");
                        public static readonly Guid MonthlyReview = new Guid("{D0700E76-8BC2-427A-9849-FE3B6D28BD22}");

                    }

                    public static readonly Guid ContentTagsFolder = new Guid("{5FB3BB39-C228-4CF7-A462-AFB4C2FD1125}");
                    public static readonly Guid RolesFolder = new Guid("{37D9CC3F-23C5-4607-B405-36D90AA3BC9D}");
                    public static readonly Guid FundAwardsFolder = new Guid("{428425C3-9E44-4817-884F-7B177F34DBEA}");
                    public static readonly Guid RiskFolder = new Guid("{53E04B33-A317-4273-A6AE-1CEB0CB4B5D8}");
                    public static readonly Guid DocumentTypesFolder = new Guid("{D3A17F75-3ABE-476D-A939-D6F52B4929D1}");
                    public static readonly Guid SalesRepresentativesFolder = new Guid("{D7A3C7F9-7C5A-47F0-B789-ED92F48FE504}");
                    public static readonly Guid DefaultVisitorRole = new Guid("{9B8F4782-739B-4B96-BFAD-0DA021609B25}");
                    public static readonly Guid ProfessionalInvestorRole = new Guid("{84AB6F41-E811-4545-AAF9-018F97CE7E83}");
                    public static readonly Guid BlogTypeFolder = new Guid("{02142EB0-FA22-4228-B036-1BD19D25D145}");
                    public static readonly Guid AdditionalFundTypesFolder = new Guid("{07A972F4-EABA-4D9E-96C0-B96B69E58E38}");

                    public static class EmailTemplates
                    {
                        public static readonly Guid FundImportResult = new Guid("{7F8EDA48-7F78-4432-B4C3-7B0A3EFCCBAB}");
                        public static readonly Guid EditEmailPrefEmailTemplateForUkResidents = new Guid("{39197694-0579-4E98-B129-C37DE5CEFE04}");
                        public static readonly Guid EditEmailPrefEmailTemplateForNonUkResidents = new Guid("{E89C7635-1CE6-4847-9A40-108BD856D275}");
                        public static readonly Guid ResendEditEmailPrefLinkEmailTemplate = new Guid("{D8E1FF30-1DA7-4C87-BCAA-E0268EAF3D45}");
                    }

                    public static class SearchSettings
                    {
                        public static class FundTags
                        {
                            public static readonly Guid Classes = new Guid("{C4A3CAEF-DB1A-4327-9BB4-7CD41251F958}");
                            public static readonly Guid Objectives = new Guid("{2E4766BE-321E-4753-9DCC-7C4E0B17C539}");
                            public static readonly Guid Processes = new Guid("{9A91E1B2-FA67-4370-9DF6-9D5465997B47}");
                            public static readonly Guid Regions = new Guid("{06ED5FC3-4F7B-46A8-835C-9A43A5492545}");
                            public static readonly Guid Types = new Guid("{965C7D73-C87F-4025-B721-1BFBC4E3652E}");
                            public static readonly Guid CompaniesFolder = new Guid("{D30D5098-622F-40E4-A0E1-4EDA06B3CAC7}");
                        }
                    }

                    public static class EmailPreferences
                    {
                        public static readonly Guid EditEmailPreferecesPage = new Guid("{4F9244C0-0FDB-4885-8805-7354D21F785B}");
                        public static readonly Guid EditEmailPreferecesSuccessPage = new Guid("{FDF8ADFF-0A07-48A8-9020-503FA8C18EF1}");
                        public static readonly Guid EditEmailPreferencesFailurePage = new Guid("{81F4D4A9-79D7-4DB9-8FAC-99101974D495}");
                        public static readonly Guid RegisterNonProfUserSuccess = new Guid("{A1B149BC-E07D-4DEB-AAD7-C3FC2AF2CEC0}");
                        public static readonly Guid RegisterProfUserSuccess = new Guid("{5BA7CF45-7B27-45B2-91B3-E6A57C75AF0A}");
                        public static readonly Guid ResendEmailPrefLinkPage = new Guid("{3AB57D49-E80B-4B64-8C7B-5B6032DD7B6C}");

                        public static readonly Guid ResendEmailPrefLinkSuccess = new Guid("{38B28644-AF21-45EE-9334-9884D6D77C79}");
                        public static readonly Guid ResendEmailPrefLinkFailed = new Guid("{EC5397F7-8A2F-4306-98CB-0D880048C8E9}");
                    }

                    public static class RegisterInvestor
                    {
                        public static readonly Guid NonProfessionalInvestorRegHeaderItem = new Guid("{1A8CF950-633C-491F-BD62-A4F46DFF2E15}");
                        public static readonly Guid ProfessionalInvestorRegHeaderItem = new Guid("{562AD05F-4E80-4B3B-BC1A-1E817D80E5F5}");
                        public static readonly Guid ProfessionalInvestorRegPageItem = new Guid("{6B4978BF-49B6-4D6F-BDBF-125F1F7C9A45}");
                        public static readonly Guid NonProfessionalInvestorRegPageItem = new Guid("{46BED811-8A61-409D-950D-57A58E0BA246}");
                    }

                    public static class DocumentType
                    {
                        public static readonly Guid Factsheet = new Guid("{D4DC9F77-EC92-429D-A975-839A13F17C2A}");
                        public static readonly Guid Kiids = new Guid("{47476B16-8600-4EBD-BC6F-AE6E84EC1AC2}");
                    }
                }

                public static readonly Guid ProfessionalsFolder = new Guid("{55FD6306-D4DB-49BA-8471-07BDC35A1727}");
                public static readonly Guid FundTeamsFolder = new Guid("{CE164BFD-E4D5-4ED3-B3D6-7E9528C34156}");
                public static readonly Guid FundsFolder = new Guid("{92ABA568-F063-4316-A3B3-27AA0B4F154A}");
                public static readonly Guid BenckmarksFolder = new Guid("{C9224BE2-659E-4F18-81F5-90991EFD1F3D}");
                public static readonly Guid FundTeamPageFolder = new Guid("{7158BC37-5194-4F34-AAB4-27BF492981C5}");
                public static readonly Guid SustainableInvestmentTeamItemId = new Guid("{64900D7A-93A6-4B31-A639-E1BA6E52D5B4}");

                public static class Labels
                {
                    public static readonly Guid FundLabels = new Guid("{442E8B0A-5EE8-4518-99D6-E4EE77D8AF7B}");
                    public static readonly Guid GenericLabels = new Guid("{FCE31F2F-338D-42D1-84B5-43B6C8A3B1EA}");
                    public static readonly Guid SharePriceLabels = new Guid("{29B3D857-727C-4BD5-BA29-BA40251D17B8}");
                    public static readonly Guid EditEmailPreferenceLabels = new Guid("{A326CA3E-E797-4E0E-93A1-C3F9DC919F93}");
                    public static readonly Guid RegisterNonprofUserLabels = new Guid("{87DCECBD-F287-453A-A20C-1C197A4D6180}");
                    public static readonly Guid RegisterProfUserLabels = new Guid("{EF986249-7B13-46A8-8CF1-45FDDA8C5A13}");
                    public static readonly Guid ListingFilterLabels = new Guid("{85B1B6D9-0531-41B7-B8FA-A54DAFFBE9E2}");
                    public static readonly Guid SearchResultPageLabels = new Guid("{51E9CE01-573F-4131-8B12-5F7973C563D1}");
                    public static readonly Guid PersonalizedContentComponentLabels = new Guid("{CCEE0BD8-2B10-456E-BAF3-F612CD0039F2}");
                }
            }
            public static class Templates
            {
                public static class Pages
                {
                    public static readonly Guid FundPage = new Guid("{1F294BCB-1F17-4000-94BD-501A96BEC59A}");
                    public static readonly Guid ProfilePage = new Guid("{EFD6D001-B425-4E47-9CAD-4577D222D8E0}");
                    public static readonly Guid FundListingPage = new Guid("{5CBA079C-D5AD-4342-B6F5-DA1D08D72F47}");
                    public static readonly Guid FundTeamPage = new Guid("{70ABA630-819A-46EC-BBDD-FF7C6F9BC58A}");
                    public static readonly Guid ConfirmRolePage = new Guid("{00CCC85C-BAC1-45B1-80D1-3E1F7129B044}");
                    public static readonly Guid ArticlePage = new Guid("{AB8309B9-E012-4C89-88BB-6D364FB5E0DB}");


                }
                public static class Modules
                {
                    public static readonly Guid FooterLinkSection = new Guid("{0E4490FF-F397-4AA0-A9F1-A47F3E063FA2}");
                    public static readonly Guid FooterIconTextSection = new Guid("{7D2146C2-1A3E-4C14-BA27-3A3903960A16}");

                    public static readonly Guid GenericListingModule = new Guid("{7474CFBA-30CC-4E88-9FC5-0848FCE4C77C}");
                    public static readonly Guid GenericListingModuleItem = new Guid("{FF052B94-76BD-4482-855F-AF3712EE4872}");
                }

                public static readonly Guid QuickLinksContainer = new Guid("{51F9BB99-7CDE-43A9-8A52-393A69F7DBE2}");
                public static readonly Guid InvestorTypeSwitcher = new Guid("{47FF03FF-FFD9-4AE1-9DCE-241526D993A4}");
                public static readonly Guid MainNavigation = new Guid("{DAFFFF73-B195-4623-8CAB-F0654D6388C3}");

                public static class Settings
                {
                    public static readonly Guid Document = new Guid("{66E09D5B-4F8E-4313-9DF3-FA7E53FBE83A}");
                    public static readonly Guid Award = new Guid("{F89D20BD-3B5C-4755-8E85-0EDBBC875887}");
                    public static readonly Guid Fund = new Guid("{DADEC9BF-B6E8-4E04-A082-F4EB20B76FF9}");
                    public static readonly Guid LookupItem = new Guid("{F3CCFE38-F67A-4289-B6BE-FC004EBCD614}");
                    public static readonly Guid Benchmark = new Guid("{4E82A221-ACE7-4A01-96F1-E46F75677B58}");
                    public static readonly Guid DividendFolder = new Guid("{C6AA3A82-AE10-4038-8506-B66A8EB24426}");
                    public static readonly Guid CompositionFolder = new Guid("{9545B107-176D-47E1-A83D-E8461C8AE991}");
                    public static readonly Guid FundTeam = new Guid("{6B0F9C6C-2E2D-4F66-A34C-7A1D7290CA4D}");
                    public static readonly Guid SalesRepresentative = new Guid("{A77BC5D7-0F05-4891-B94B-BE09FEF4AF08}");
                    public static readonly Guid SharedDocuments = new Guid("{95780F8D-3EFB-46A7-9872-06C6119F7A3C}");
                    public static readonly Guid Professional = new Guid("{A6462A09-C446-45A5-9238-BDFF5170602B}");
                }
            }
        }
        public static class SitecoreTokens
        {
            public const string CurrentYear = "$currentYear$";

            public static class SearchResultPage
            {
                public const string NoOfResultsPerPage = "$results$";
                public const string NoOfAllResults = "$allResults$";
            }

            public static class RegisterUserProcess
            {
                public const string ResendEmailLinkToken = "{ResendEmailLink}";

                public static class EmailTokens
                {
                    public const string FullNameToken = "{FullName}";
                    public const string EditPrefLinkToken = "{EditPrefLink}";
                    public const string SiteURLToken = "{SiteURL}";
                }
            }
        }
    }
}