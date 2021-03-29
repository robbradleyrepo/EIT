namespace LionTrust.Feature.Navigation
{
    using Sitecore.Data;

    public static class Constants
    {
        public static class Identity
        {     
            public const string Logo_FieldName = "Logo";
            public static readonly ID Logo_FieldID = new ID("{F113BF3D-FE80-4A15-9CDA-679B3C3CB3BE}");
        }

        public static class Menu
        {
            public const string HeaderMenuItems_FieldName = "HeaderMenuItems";
            public static readonly ID HeaderMenuItems_FieldID = new ID("{31282663-006B-47B1-95AE-F044B042F534}");

            public const string ContactUsPage_FieldName = "Contact Us Page";
            public static readonly ID ContactUsPage_FieldID = new ID("{0D831D08-90C7-4E31-B880-5A93146B3029}");

            public const string MyLionTrust_FieldName = "MyLionTrust";
            public static readonly ID MyLionTrust_FieldID = new ID("{B810A405-73F4-48CC-AF61-778C82531262}");

            public const string MyPreferences_FieldName = "MyPreferences";
            public static readonly ID MyPreferences_FieldID = new ID("{6F6A49B5-C780-4EDE-9EB4-9EA509204434}");

            public const string SignUpNewsletter_FieldName = "SignUpNewsletter";
            public static readonly ID SignUpNewsletter_FieldID = new ID("{6EB0248B-457F-491C-8F9E-13AEFE2CDEC8}");

            public const string YouAreViewingLabel_FieldName = "YouAreViewingLabel";
            public static readonly ID YouAreViewingLabel_FieldID = new ID("{15999105-600F-43FB-B206-ED93304A98E6}");

            public const string ChangeLabel_FieldName = "ChangeLabel";
            public static readonly ID ChangeLabel_FieldID = new ID("{E4A364E6-2E1A-4854-ABD8-6A02ACF832CD}");

            public const string CloseLabel_FieldName = "CloseLabel";
            public static readonly ID CloseLabel_FieldID = new ID("{B5C82F2D-EEDD-4A5A-AB05-7AFC42087CC2}");

            public const string MenuItems_FieldName = "MenuItems";
            public static readonly ID MenuItems_FieldID = new ID("{5D68CC82-EA43-4789-B892-0E49244F68D5}");
        }

        public static class SiteRoot
        {
            public const string TemplateName = "Site Root";
            public static readonly ID TemplateID = new ID("{8BAA6E9E-CA51-4CBD-8E4E-BA53CAFE8C17}");
        }

        public static class Home
        {
            public const string TemplateName = "Home";
            public static readonly ID TemplateID = new ID("{51D3A578-6E10-4EC6-BB5B-5C1307A6D661}");
        }

        public static class Navigation
        {
            public const string MenuTitle_FieldName = "Menu Title";
            public static readonly ID MenuTitle_FieldId = new ID("{960FE422-ACE9-47E8-9E18-5D7E5FFA218F}");
            public const string MenuSummary_FieldName = "Menu Summary";
            public static readonly ID MenuSummary_FieldId = new ID("{8D6CE9FF-806D-4FC1-82A0-E36537346397}");
            public const string ShowInNavigation_FieldName = "ShowInNavigation";
            public static readonly ID ShowInNavigation_FieldId = new ID("{D306DABF-C78F-434A-B1DB-17C13ED095A0}");
        }
    }
}