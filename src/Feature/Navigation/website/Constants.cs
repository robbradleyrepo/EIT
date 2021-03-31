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

        public static class Header
        {
            public const string HeaderMenuItems_FieldName = "HeaderMenuItems";
            public static readonly ID HeaderMenuItems_FieldID = new ID("{31282663-006B-47B1-95AE-F044B042F534}");

            public const string ContactUsPage_FieldName = "Contact Us Page";
            public static readonly ID ContactUsPage_FieldID = new ID("{0D831D08-90C7-4E31-B880-5A93146B3029}");
        }

        public static class Footer
        {
            public const string FindUsLabel_FieldName = "FindUsLabel";
            public static readonly ID FindUsLabel_FieldID = new ID("{05ABA786-3D35-479B-ADA4-ECEE716AD9F5}");

            public const string Address_FieldName = "Address";
            public static readonly ID Address_FieldID = new ID("{F53DF5DE-4D7D-4278-8DAE-59D0C79D0B0C}");

            public const string Location_FieldName = "Location";
            public static readonly ID Location_FieldID = new ID("{C77B9712-2755-4657-BDF2-C43AED9BEE02}");

            public const string PostalCode_FieldName = "PostalCode";
            public static readonly ID PostalCode_FieldID = new ID("{580A13A4-8095-4815-BE0A-2888C67A8AEB}");

            public const string GoogleMapsLink_FieldName = "GoogleMapsLink";
            public static readonly ID GoogleMapsLink_FieldID = new ID("{A4F06287-E1BE-4C76-96D8-E0E8B609AAC1}");

            public const string GoogleMapsCTALabel_FieldName = "GoogleMapsCTALabel";
            public static readonly ID GoogleMapsCTALabel_FieldID = new ID("{CA58E5AB-3275-422C-B18F-79D43DCC58F1}");

            public const string GetInTouchLabel_FieldName = "GetInTouchLabel";
            public static readonly ID GetInTouchLabel_FieldID = new ID("{EAF0CE55-4593-4771-924B-E078F3AA3407}");

            public const string PhoneNumber_FieldName = "PhoneNumber";
            public static readonly ID PhoneNumber_FieldID = new ID("{5D9590C5-5DBE-47A1-B617-634DEBC799C5}");

            public const string Email_FieldName = "Email";
            public static readonly ID Email_FieldID = new ID("{D77B2873-DB24-4887-9DB1-AF3BCD76F3D6}");

            public const string EmailCTALabel_FieldName = "EmailCTALabel";
            public static readonly ID EmailCTALabel_FieldID = new ID("{E4CA4419-EFEE-45A0-BEF9-2F611004D7A7}");

            public const string TwitterAccounts_FieldName = "TwitterAccounts";
            public static readonly ID TwitterAccounts_FieldID = new ID("{BA6CB33D-C63F-4BD4-92A1-8C3DDE97F9FC}");

            public const string TwitterIcon_FieldName = "TwitterIcon";
            public static readonly ID TwitterIcon_FieldID = new ID("{352406EE-D59A-49A0-9E9C-55738142A220}");

            public const string LinkedinLink_FieldName = "LinkedinLink";
            public static readonly ID LinkedinLink_FieldID = new ID("{44A65390-FCC7-4C9D-82EF-C1BDE9E4FB4C}");

            public const string LinkedinIcon_FieldName = "LinkedinIcon";
            public static readonly ID LinkedinIcon_FieldID = new ID("{8AEA31E3-9089-4887-A062-A57ABD3CC7B9}");

            public const string FacebookLink_FieldName = "FacebookLink";
            public static readonly ID FacebookLink_FieldID = new ID("{2A464F51-5954-4260-B8A0-EE0A7DE64E02}");

            public const string FacebookIcon_FieldName = "FacebookIcon";
            public static readonly ID FacebookIcon_FieldID = new ID("{5717E811-4F55-4821-B482-ACDEFD3CFB11}");

            public const string SubscribeNewsletterLabel_FieldName = "SubscribeNewsletterLabel";
            public static readonly ID SubscribeNewsletterLabel_FieldID = new ID("{6A0F17AB-B448-474A-A275-5EE5F12F8D10}");

            public const string SubscribeCTALabel_FieldName = "SubscribeCTALabel";
            public static readonly ID SubscribeCTALabel_FieldID = new ID("{A38B21B3-2BD4-4BAB-9808-41319E81A763}");

            public const string SubscribeCTALink_FieldName = "SubscribeCTALink";
            public static readonly ID SubscribeCTALink_FieldID = new ID("{25FE6D9F-7990-451A-A9CB-94A632389CD3}");

            public const string FooterLinks_FieldName = "FooterLinks";
            public static readonly ID FooterLinks_FieldID = new ID("{E28B0C65-2DC9-42CB-B852-282C57BFEE3A}");

            public const string Copyright_FieldName = "Copyright";
            public static readonly ID Copyright_FieldID = new ID("{E168F770-7271-4DC4-9242-72805145ACDC}");
        }

        public static class Menu
        {           
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