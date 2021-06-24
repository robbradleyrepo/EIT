﻿namespace LionTrust.Feature.Search
{
    public static class Constants
    { 
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
                public const string FundCategoriesFieldId = "{FDDD0447-86EA-4E24-9F48-BCDC60D48922}";
                public const string FundManagersFieldId = "{6FC49C6C-12C1-455E-951D-70BEB68AEE68}";
                public const string FundTeamsFieldId = "{3E5ADEE5-081E-40A4-BFA5-77A3449A93ED}";
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
        }

        public static class Pagination
        {
            public const string PreviousLabel_FieldId = "{EE10A0F8-834B-4C8B-9EC5-9B643FCD112C}";
            public const string NextLabel_FieldId = "{87E96566-897A-4D4C-8DA5-8727BD4E436E}";
        }
    }
}