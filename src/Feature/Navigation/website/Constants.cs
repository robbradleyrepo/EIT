namespace LionTrust.Feature.Navigation
{
    using Sitecore.Data;

    public static class Constants
    {
        public static class Breadcrumb
        {
            public const string IncludeInBreadcrumb = "{E409CE1D-3E4E-4C73-A01D-BA8E98BDF25F}";
            public const string BreadcrumbTitle = "{951B9A2D-0238-49ED-A8B9-C2A9255BA2DF}";
        }

        public static class Identity
        {
            public const string Logo_FieldName = "Logo";
            public static readonly ID Logo_FieldID = new ID("{F113BF3D-FE80-4A15-9CDA-679B3C3CB3BE}");
        }

        public static class HeaderDropDown
        {
            public static readonly ID PageItem_FieldID = new ID("{F01D9F47-3D98-42D9-AB7D-59B5E845B974}");
            public const string PageItem_FieldName = "Page Item";

            public static readonly ID NavigationColumns_FieldID = new ID("{D335CC19-7219-49BA-8782-95DEF11A5C4D}");
            public const string NavigationColumns_FieldName = "NavigationColumns";

            public static readonly ID Images_FieldID = new ID("{C230E8EB-6B8F-44CD-BC43-5B24B540F6AE}");
            public const string Images_FieldName = "Images";

            public static readonly ID ShowCTA_FieldID = new ID("{8016EC09-4619-48B1-B90D-D60B2E2DA621}");
            public const string ShowCTA_FieldName = "Show CTA";

            public static readonly ID CTALabel_FieldID = new ID("{24930482-3138-42F2-9AAF-5439885993D4}");
            public const string CTALabel_FieldName = "CTA Label";

            public static readonly ID CTALink_FieldID = new ID("{716377AD-4C27-4F6D-A932-C121AED8EB70}");
            public const string CTALink_FieldName = "CTA Link";
        }

        public static class HeaderImage
        {
            public static readonly ID Image_FieldID = new ID("{C0A0281F-CFE6-45E1-A095-B02D109A8952}");
            public const string Image_FieldName = "Image";

            public static readonly ID Title_FieldID = new ID("{D6596C9A-E05A-49DD-98E6-7BA505D79842}");
            public const string Title_FieldName = "Title";

            public static readonly ID Description_FieldID = new ID("{7FF41320-58C1-4A05-B2C5-DD1E4F9C2191}");
            public const string Description_FieldName = "Description";

            public static readonly ID Link_FieldID = new ID("{2DAF00E2-B3E8-4BB2-836A-C47F505EE481}");
            public const string Link_FieldName = "Link";
        }

        public static class HeaderNavigationColumn
        {
            public static readonly ID ShowColumnLink_FieldID = new ID("{FE160AB8-1158-46C9-80AD-43557CE4129D}");
            public const string ShowColumnLink_FieldName = "Show Column Link";

            public static readonly ID ColumnLink_FieldID = new ID("{9012EE1A-DEA2-49FA-951D-F72E835639CA}");
            public const string ColumnLink_FieldName = "Column Link";

            public static readonly ID ColumnLinkLabel_FieldID = new ID("{9063296F-3BBE-4D1F-9EA6-9A6E2442B520}");
            public const string ColumnLinkLabel_FieldName = "Column Link Label";

            public static readonly ID ColumnTitle_FieldID = new ID("{89CC8946-1499-4D72-9D06-016CC68B0401}");
            public const string ColumnTitle_FieldName = "Column Title";

            public static readonly ID PageLinks_FieldID = new ID("{E6689B09-D0FF-4B12-965C-625B7185F6BE}");
            public const string PageLinks_FieldName = "Page Links";
        }

        public static class FooterConfiguration
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

            public const string SubscribeNewsletterLabel_FieldName = "SubscribeNewsletterLabel";
            public static readonly ID SubscribeNewsletterLabel_FieldID = new ID("{6A0F17AB-B448-474A-A275-5EE5F12F8D10}");

            public const string SubscribeCTALabel_FieldName = "SubscribeCTALabel";
            public static readonly ID SubscribeCTALabel_FieldID = new ID("{A38B21B3-2BD4-4BAB-9808-41319E81A763}");

            public const string SubscribeCTALink_FieldName = "SubscribeCTALink";
            public static readonly ID SubscribeCTALink_FieldID = new ID("{25FE6D9F-7990-451A-A9CB-94A632389CD3}");

            public const string Copyright_FieldName = "Copyright";
            public static readonly ID Copyright_FieldID = new ID("{E168F770-7271-4DC4-9242-72805145ACDC}");

            public const string PageLinks_FieldName = "Page Links";
            public static readonly ID PageLinks_FieldId = new ID("{E28B0C65-2DC9-42CB-B852-282C57BFEE3A}");

            public const string SocialLinks_FieldName = "Social Links";
            public static readonly ID SocialLinks_FieldId = new ID("{F0D67B9B-4D7D-40AC-B854-AC936934071D}");
        }

        public static class SocialIcon
        {
            public const string SocialLink_FieldName = "Link";
            public static readonly ID SocialLink_FieldID = new ID("{E990E03D-0332-45CC-A5E7-613996DEDA74}");

            public const string SocialIcon_FieldName = "Icon";
            public static readonly ID SocialIcon_FieldID = new ID("{321DB99F-5D14-4703-AC2F-6FEEA5E48000}");

            public const string TwitterAccounts_FieldName = "TwitterMultipleAccounts";
            public static readonly ID TwitterAccounts_FieldID = new ID("{9568F987-E036-4144-B158-B2084D778CE2}");
        }

        public static class PageLink
        {
            public const string Link_FieldName = "Link";
            public static readonly ID Link_FieldID = new ID("{3F39E86E-0426-46F0-8F64-C5636372E13A}");
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

        public static class NavigationSiteRoot
        {
            public const string TemplateName = "_NavigationSiteRoot";
            public static readonly ID TemplateID = new ID("{8375C190-5C85-4616-9E14-E8AE5CBAD135}");
        }

        public static class NavigationRoot
        {
            public const string TemplateName = "_NavigationRoot";
            public static readonly ID TemplateID = new ID("{93962463-4F5F-45E8-BE15-79F96D0CBB04}");

            public const string FooterConfiguration_FieldName = "Footer Configuration";
            public static readonly ID FooterConfiguration_FieldId = new ID("{E27B5DB1-CE93-4CB1-BA69-F78556AACAF0}");

            public const string HeaderConfiguration_FieldName = "Header Configuration";
            public static readonly ID HeaderConfiguration_FieldID = new ID("{95F0787B-70E6-4164-98C0-F9502163D720}");

            public const string ContactUsPage_FieldName = "Contact Us Page";
            public static readonly ID ContactUsPage_FieldID = new ID("{0D831D08-90C7-4E31-B880-5A93146B3029}");
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