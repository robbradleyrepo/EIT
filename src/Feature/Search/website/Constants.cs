namespace LionTrust.Feature.Search
{
    using System;

    public static class Constants
    {
        public static class SearchAnalytics
        {
            public static Guid SearchPageEvent = new Guid("{0C179613-2073-41AB-992E-027D03D523BF}");
        }

        public static class SearchOverlay
        {
            public const string PlaceholdertextFieldId = "{29F9E9E1-C66C-40D6-9669-4C170097B381}";
        }
        public static class SiteSearch
        {
            public const string ViewAllFilterTextFieldId = "{34D19244-FFEA-42CC-8310-3D4BCF89B4F8}";
            public const string HeadingFieldId = "{A7C70493-A174-455B-BF2D-67A38143A1E7}";
            public const string SearchLabelFieldId = "{7E350BBA-B71B-49AA-910F-5316636CAC17}";
            public const string ResultsPerPageFieldId = "{2056E3A0-3765-4DF0-8836-349180E8D7F5}";
            public const string SearchResultsFoundLabelFieldId = "{83D03CE9-1F83-4F04-8D65-474AB9BFBFC0}";

        public const string FactsheetLinkTextFieldId = "{94CAE85F-6AED-4D42-BA9B-C487AF809CC7}";
        }

        public static class SiteSearchFilter
        {
            public const string PageNameFieldId = "{A7F99C6C-A5AF-4968-9C11-EC961745460F}";
            public const string TemplateFieldId = "{2CD1668B-43FA-4854-8D91-84ED2AD9FB3C}";
        }

        public static class APIFacets
        {
            public static class Defaults
            {
                public const string ArticleSearchFacetsConfig = "{D61FC1B3-9969-4F95-8796-FEFBC864CA1F}";
                public const string FundSearchFacetsConfig = "{64E0343F-FF1C-4B78-9461-C57CD8B1050D}";
            }
            public static class ArticleSearchFacetsConfig
            {
                public const string FundsFieldId = "{3F4BC13E-73FC-45D7-B858-8D54F2F4F51B}";
                public const string FundsLabelFieldId = "{3B09D368-CBF7-46BC-AD32-E922B9922667}";
                public const string FundCategoriesFieldId = "{FDDD0447-86EA-4E24-9F48-BCDC60D48922}";
                public const string FundCategoriesLabelFieldId = "{630F512C-B754-49C0-AEDC-719960FE72D7}";
                public const string FundManagersFieldId = "{6FC49C6C-12C1-455E-951D-70BEB68AEE68}";
                public const string FundManagersLabelFieldId = "{08DEFE33-E04E-42EC-BF84-2872A25A73E1}";
                public const string FundTeamsFieldId = "{3E5ADEE5-081E-40A4-BFA5-77A3449A93ED}";
                public const string FundTeamsLabelFieldId = "{6591FE6B-328E-48A4-AE60-C25673C22F68}";
            }

            public static class FundManagerFacetOption
            {
                public const string TemplateId = "{73EFF6F8-D95A-43B1-A069-5EF0B709CBB4}";

                public const string IsFundManagerFieldId = "{AD3AF339-1FD6-47EA-B506-7ED32F5D6246}";
            }

            public static class FundSearchFacetsConfig
            {
                public const string FundRangesFieldId = "{FBE9329C-271B-4573-B4FD-6DC4B7C692C9}";
                public const string FundManagersFieldId = "{EC744986-7D64-459D-A2E6-8CB5A9C0848D}";
                public const string FundTeamsFieldId = "{38DD3B5E-9E3E-402C-8F5F-8D595EBDAB19}";
                public const string FundRegionsFieldId = "{D859AD9C-7473-420D-A7C3-ACE454F18D07}";
                public const string FundRangesLabelFieldId = "{F9DAF380-33EB-424C-B24C-6FFE0D9C98BD}";
                public const string FundManagersLabelFieldId = "{848574BD-2F11-48D3-8868-E0377F188BA7}";
                public const string FundTeamsLabelFieldId = "{A533F896-A92A-4897-9A4A-70F5038C2D28}";
                public const string FundRegionsLabelFieldId = "{5E6F002F-6B6B-43F5-B665-5D1E03622456}";
            }
        }

        public static class ArticleLister
        {
            public const string FiltersLabel_FieldId = "{176B6BA4-37A4-4853-9753-690C7D1A3C9E}";
            public const string FundLabel_FieldId = "{0674EA3D-C9DB-4C66-B6F2-FF84C0A8E218}";
            public const string MonthLabel_FieldId = "{6468D4A5-9157-4A80-81AA-70615FB875A9}";
            public const string YearLabel_FieldId = "{5AB5A6FE-85D0-4BAE-8B0E-8446BED3FA3C}";
            public const string ApplyFiltersLabel_FieldId = "{8895C4A2-26CF-4F3E-A990-B0A26FDB0802}";
            public const string ClearFiltersLabel_FieldId = "{B2BC0424-664C-4737-A680-9B2A17296852}";
            public const string DateNewestFirstLabel_FieldId = "{F72DC2B2-2396-401F-AFD5-3A4075892A86}";
            public const string DateOldestFirstLabel_FieldId = "{EE6AF776-8D37-4B81-A29F-551E7BB1D481}";
            public const string DateIcon_FieldId = "{B3FF2403-3560-4DC1-B9B1-F9D93D85775D}";
            public const string SearchPlaceholder_FieldId = "{0EBA5571-B102-467C-993A-3025A1176831}";
            public const string SortLabel_FieldId = "{745B2803-4617-45DB-9C43-E6D910E50EC3}";
            public const string FilterLabel_FieldId = "{6AF33400-0709-4517-B1F7-8D01C5D66C06}";
            public const string VideoIcon_FieldId = "{68CADA79-69A0-454A-A9EB-1E5C3E6A6CE2}";
            public const string WhatcNowLabel_FieldId = "{566136F8-0D80-4B50-8ECC-9DB53FC96161}";
        }

        public static class Pagination
        {
            public const string PreviousLabel_FieldId = "{EE10A0F8-834B-4C8B-9EC5-9B643FCD112C}";
            public const string NextLabel_FieldId = "{87E96566-897A-4D4C-8DA5-8727BD4E436E}";
        }

        public static class FundLister
        {
            public const string ApplyFiltersLabel_FieldId = "{881C5CC5-745E-4156-A0E0-0E4E4F4306D5}";
            public const string ClearFiltersLabel_FieldId = "{35663C96-FC62-4171-94B8-6BA42639F94C}";
            public const string ViewAsGridLabel_FieldId = "{9421B84A-C63F-47C1-A73E-F442F401ED26}";
            public const string ViewAsListLabel_FieldId = "{59731A11-3890-4FDF-9D9F-0592A4A35B50}";
            public const string FiltersLabel_FieldId = "{F1742CC9-02A1-488D-AF97-7A793B8018A9}";
            public const string FundCentreLink_FieldId = "{79982F65-F681-40F9-A526-63BE0E149EB3}";
            public const string SearchPlaceholder_FieldId = "{450AF31C-6B9F-4EC0-BB4E-4A681DCD6127}";
            public const string MobileViewLabel_FieldId = "{846F7732-B5EC-470F-8DC9-277ACF08A232}";
            public const string FundLiterature_FieldId = "{01B10884-E8D0-4983-9C9C-FAF781087DA8}";
            public const string AllDocumentsLabel_FieldId = "{061CF8AB-5D59-47E6-88AA-BDF808FC3BE3}";
            public const string FollowLabel_FieldId = "{373D3F32-A145-4B77-830C-96083118CACA}";
            public const string FactsheetLabel_FieldId = "{AD4992D4-588A-4AB0-B4EC-793A08E648D2}";
        }

        public static class Settings
        {
            public const string ArticleApiRoute_SettingName = "Feature.Search.ArticleApiRoute";
            public const string FundApiRoute_SettingName = "Feature.Search.FundApiRoute";
            public const string MyFundsApiRoute_SettingName = "Feature.Search.MyFundsApiRoute";
            public const string SiteSearchApiRoute_SettingName = "Feature.Search.SiteSearchApiRoute";
        }

        public static class MyFundsLister
        {
            public const string ApplyFiltersLabel_FieldId = "{A00B7878-BC89-4ED8-A31D-88C9CF99425E}";
            public const string ClearFiltersLabel_FieldId = "{36BBBEF9-10C6-486B-B783-879A24077A83}";
            public const string FiltersLabel_FieldId = "{7F5C9EDE-FB85-48C2-8652-AC39A1E91D5B}";
            public const string FundLiterature_FieldId = "{C497ED1B-8CFC-4E2F-BA18-B5E60F40B04F}";
            public const string AllDocumentsCtaText_FieldId = "{E8DA7B24-C7E1-4481-8B95-23CBA236B58A}";
            public const string FundUpdateCtaText_FieldId = "{45FBC71C-F3E7-4EA4-8E2A-90624C890371}";
            public const string ViewFundCtaText_FieldId = "{8975EEEF-70FB-4D50-8C96-1C633A0B999E}";
            public const string SortLabel_FieldId = "{C416E63E-F51D-46CC-BE24-317C910DA7D2}";
            public const string FacetsConfiguration_FieldId = "{FC052C32-950E-499D-B07F-0A47A29A76DC}";
            public const string Title_FieldId = "{E6BF180C-FA50-411F-9892-1DC5F6AD06CA}";
            public const string SortAZLabel_FieldId = "{0680177D-A5DE-4F84-8CAE-986FD4B00B57}";
            public const string SortZALabel_FieldId_FieldId = "{EA75175B-B960-4494-BD20-FD5B3B949934}";
        }
    }
}