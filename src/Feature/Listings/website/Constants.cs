namespace LionTrust.Feature.Listings
{
    public static class Constants
    {
        public static class Accordion
        {
            public const string HeadingField = "{38E1ED19-3192-47C2-A92B-55A8D15280D0}";

            public const string ImageField = "{8FC7A09F-043C-41B7-BCF5-C8633F058F99}";

            public const string CopyField = "{E21C6470-0045-4FB7-8CE1-3B72F54D7CA4}";
        }

        public static class AccordionRenderingParameters
        {
            public const string ThemeFieldId = "{58E2AF21-FE7B-4538-8574-6B87207970D3}";

            public const string TextAlignFieldId = "{AB4A19DC-DE18-4E5B-A763-095F990CC0CA}";

            public const string BackgroundColourFieldId = "{1841944B-D6A8-48CA-BAB1-071C8C7C603E}";

            public const string AccordionAlignFieldId = "{BB4CC394-B829-45DF-B106-CC4E803B88C2}";
        }

        public static class HorizontalScroll
        {
            public const string Title_FieldID = "{FD89A7AF-A29A-42DC-949C-5CE8F66F9C71}";
            public const string Title_FieldName = "HorizontalScroll_Title";

            public const string NumberOfItems_FieldID = "{08F9501F-35B1-4E66-96D0-8D04858A7EC5}";
            public const string NumberOfItems_FieldName = "HorizontalScroll_NumberOfItems";
        }

        public static class ScrollerCard
        {
            public const string Title_FieldID = "{5B9F1D26-BDE7-4209-A71C-F0CEAC2600B5}";
            public const string Title_FieldName = "ScrollCard_Title";

            public const string Description_FieldID = "{4826908C-F8C8-43E4-8360-0C09ED0C732A}";
            public const string Description_FieldName = "ScrollCard_Description";

            public const string Link_FieldID = "{CE038003-3CA3-4B12-A32A-BB5E664C0595}";
            public const string Link_FieldName = "ScrollCard_Link";

            public const string LinkGoal_FieldID = "{BB76C1D8-044A-4813-B4DE-E36D21085304}";
            public const string LinkGoal_FieldName = "ScrollCard_LinkGoal";
        }    

        public static class Defaults
        {
            public const string GenericListingFacetsConfig = "{D61FC1B3-9969-4F95-8796-FEFBC864CA1F}";
            public const string ListingTypeList_FieldId = "{882BBA30-5BFE-41D6-AC98-A85BEC219EE0}";
            public const string YearsList_FieldId = "{EBCE5416-BA23-40D1-8FBB-58487F247107}";
            public const string MonthsList_FieldId = "{EC94129F-5DAF-484D-852F-551008F7B82C}";
        }

        public static class GenericListingFacet
        {
            public const string ClearAllLabel_FieldId = "{4080F6C2-22E4-4C9C-89F1-CC0758E75F44}";
            public const string ApplyFiltersLabel_FieldId = "{93F357A7-248D-43F2-9F0D-75C4BE0D734D}";
            public const string FilterLabel_FieldId = "{C24AFEE2-BA7F-4440-97A2-9495910C65C6}";
            public const string FiltersLabel_FieldId = "{5B8C8965-29EC-449E-8014-282D40EE5F12}";
            public const string YearsList_FieldId = "{EBCE5416-BA23-40D1-8FBB-58487F247107}";
            public const string MonthsList_FieldId = "{EC94129F-5DAF-484D-852F-551008F7B82C}";
        }

        public static class DocumentLister
        {
            public const string SortText_FieldID = "{707223F8-4670-4008-910F-C572046D0815}";
            public const string AZText_FieldID = "{AAE23341-C861-4FBC-B0AB-16EA97A7FFC7}";
            public const string ZAText_FieldID = "{D3733E07-651D-4602-A5E6-9EA7953199BC}";
            public const string ViewPageText_FieldID = "{FEA256FA-27B1-46E9-935C-69BF920571A5}";
            public const string WatchVideoText_FieldID = "{40FD447E-6459-4EE2-B86D-DE9AB7008604}";           
            public const string NextText_FieldID = "{EB373C6D-B8F8-4E56-B711-F574E2FAB67F}";
            public const string PreviousText_FieldID = "{EA027445-01E2-488B-A94C-A2FBF460972A}";
        }

        public static class Settings
        {
            public const string DocumentApiRoute_SettingName = "Feature.Listings.DocumentsApiRoute";
            public const string GenericListingApiRoute_SettingName = "Feature.Listings.GenericListingApiRoute";
        }
    }
}