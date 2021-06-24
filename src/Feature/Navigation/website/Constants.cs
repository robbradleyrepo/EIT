namespace LionTrust.Feature.Navigation
{
    using Sitecore.Data;

    public static class Constants
    {     
        public static class CopyWithCta
        {
            public const string CopyFieldId = "{D7CD16BD-DBB5-44D9-AB26-8BBB1C9B7C2A}";
            public const string CtaFieldId = "{9FFE2DD1-92AE-4208-98BD-C1D9C73DA582}";
        }

        public static class Cta
        {
            public const string PrimaryCtaFieldId = "{43CE5EC7-2F35-438D-B0C0-7913DD98F40F}";
            public const string SecondaryCtaFieldId = "{D775550A-E906-4F7F-BBFD-7B3763F6DA35}";
            public const string PrimaryCtaGoalFieldId = "{52A16EAE-A5CD-4ADF-8614-260548E9049C}";
            public const string SecondaryCtaGoalFieldId = "{D6B121EC-192B-4E84-A3EF-90A68BD305DE}";
        }

        public static class Breadcrumb
        {
            public const string IncludeInBreadcrumb = "{E409CE1D-3E4E-4C73-A01D-BA8E98BDF25F}";
            public const string BreadcrumbTitle = "{951B9A2D-0238-49ED-A8B9-C2A9255BA2DF}";
        }

        public static class Identity
        {
            public const string Logo_FieldName = "Identity_Logo";
            public const string Logo_FieldID = "{F113BF3D-FE80-4A15-9CDA-679B3C3CB3BE}";
        }

        public static class HeaderDropDown
        {
            public const string PageItem_FieldID = "{F01D9F47-3D98-42D9-AB7D-59B5E845B974}";
            public const string PageItem_FieldName = "HeaderDropDown_PageItem";

            public const string NavigationColumns_FieldID = "{D335CC19-7219-49BA-8782-95DEF11A5C4D}";
            public const string NavigationColumns_FieldName = "HeaderDropDown_NavigationColumns";

            public const string Images_FieldID = "{C230E8EB-6B8F-44CD-BC43-5B24B540F6AE}";
            public const string Images_FieldName = "HeaderDropDown_Images";

            public const string ShowCTA_FieldID = "{8016EC09-4619-48B1-B90D-D60B2E2DA621}";
            public const string ShowCTA_FieldName = "HeaderDropDown_ShowCTA";

            public const string CTALink_FieldID = "{716377AD-4C27-4F6D-A932-C121AED8EB70}";
            public const string CTALink_FieldName = "HeaderDropDown_CTALink";
        }

        public static class QuickLinkCTA
        {
            public const string Image_FieldID = "{C0A0281F-CFE6-45E1-A095-B02D109A8952}";
            public const string Image_FieldName = "QuickLinkCTA_Image";

            public const string Title_FieldID = "{D6596C9A-E05A-49DD-98E6-7BA505D79842}";
            public const string Title_FieldName = "QuickLinkCTA_Title";

            public const string Description_FieldID = "{7FF41320-58C1-4A05-B2C5-DD1E4F9C2191}";
            public const string Description_FieldName = "QuickLinkCTA_Description";

            public const string Link_FieldID = "{2DAF00E2-B3E8-4BB2-836A-C47F505EE481}";
            public const string Link_FieldName = "QuickLinkCTA_Link";

            public const string LinkGoal_FieldID = "{1B29316A-053F-406D-A6CB-BFC62CBB3A3B}";
            public const string LinkGoal_FieldName = "QuickLinkCTA_LinkGoal";

            public const string SmallSize_FieldID = "{AEF64917-7C2C-4435-B7BF-0FDD2AA40974}";
            public const string SmallSize_FieldName = "QuickLinkCTA_SmallSize";
        }

        public static class HeaderNavigationColumn
        {
            public const string ShowColumnLink_FieldID = "{FE160AB8-1158-46C9-80AD-43557CE4129D}";
            public const string ShowColumnLink_FieldName = "HeaderDropDownNavigationColumn_ShowColumnLink";

            public const string ColumnLink_FieldID = "{9012EE1A-DEA2-49FA-951D-F72E835639CA}";
            public const string ColumnLink_FieldName = "HeaderDropDownNavigationColumn_ColumnLink";

            public const string ColumnTitle_FieldID = "{89CC8946-1499-4D72-9D06-016CC68B0401}";
            public const string ColumnTitle_FieldName = "HeaderDropDownNavigationColumn_ColumnTitle";

            public const string PageLinks_FieldID = "{E6689B09-D0FF-4B12-965C-625B7185F6BE}";
            public const string PageLinks_FieldName = "HeaderDropDownNavigationColumn_PageLinks";
        }

        public static class FooterConfiguration
        {           
            public const string FindUsLabel_FieldID = "{05ABA786-3D35-479B-ADA4-ECEE716AD9F5}";
            public const string Address_FieldID = "{F53DF5DE-4D7D-4278-8DAE-59D0C79D0B0C}";
            public const string Location_FieldID = "{C77B9712-2755-4657-BDF2-C43AED9BEE02}";
            public const string PostalCode_FieldID = "{580A13A4-8095-4815-BE0A-2888C67A8AEB}";
            public const string GoogleMapsLink_FieldID = "{A4F06287-E1BE-4C76-96D8-E0E8B609AAC1}";
            public const string GoogleMapsGoal_FieldID = "{23009B53-1B96-4886-8EFF-8D391AC9365F}";
            public const string GetInTouchLabel_FieldID = "{EAF0CE55-4593-4771-924B-E078F3AA3407}";
            public const string PhoneNumber_FieldID = "{5D9590C5-5DBE-47A1-B617-634DEBC799C5}";
            public const string PhoneGoal_FieldID = "{88EF01E6-C38F-48EA-B3D4-6F952BE64474}";
            public const string Email_FieldID = "{D77B2873-DB24-4887-9DB1-AF3BCD76F3D6}";
            public const string EmailCTALabel_FieldID = "{E4CA4419-EFEE-45A0-BEF9-2F611004D7A7}";
            public const string EmailCTAGoal_FieldID = "{B7B4B462-BF8E-4187-ADC2-5EBBE2E983E1}";
            public const string SubscribeNewsletterLabel_FieldID = "{6A0F17AB-B448-474A-A275-5EE5F12F8D10}";
            public const string SubscribeCTALink_FieldID = "{25FE6D9F-7990-451A-A9CB-94A632389CD3}";
            public const string SubscribeCTAGoal_FieldID = "{9959A4E2-5DA2-4E75-A28F-D9260DE4FB57}";
            public const string Copyright_FieldID = "{E168F770-7271-4DC4-9242-72805145ACDC}";
            public const string PageLinks_FieldId = "{E28B0C65-2DC9-42CB-B852-282C57BFEE3A}";
            public const string SocialLinks_FieldId = "{F0D67B9B-4D7D-40AC-B854-AC936934071D}";
        }

        public static class SocialIcon
        {
            public const string SocialLink_FieldID = "{E990E03D-0332-45CC-A5E7-613996DEDA74}";
            public const string SocialIcon_FieldID = "{321DB99F-5D14-4703-AC2F-6FEEA5E48000}";
            public const string SocialLinkGoal_FieldID = "{283A74F1-EBFB-4EB8-ACD7-6D4E4AB5E58C}";
        }

        public static class SecondaryNavigation
        {
            public const string HeadingFieldId = "{60E11875-0FD3-4029-8E88-D217A2735B47}";
        }

        public static class AnchorLinks
        {
            public const string HeadingFieldId = "{2DFCB4D4-7986-47A1-A0F3-79E139F3F976}";
            public const string CtaFieldId = "{8D463FE0-E08E-4BE8-88F5-727ACEF0E1C8}";
            public const string CtaGoalFieldId = "{632237D3-8942-415E-AB0D-3F6051E31D63}";
        }

        public static class LinkWithGoal
        {
            public const string GoalFieldId = "{FEE8CC02-9E0E-4510-9343-F642A9ABAFEC}";
        }

        public static class PageLink
        {
            public const string Link_FieldID = "{3F39E86E-0426-46F0-8F64-C5636372E13A}";
            public const string LinkGoal_FieldId = "{8A4C049E-DAD3-4280-B66A-86ECB20987BF}";
        }

        public static class NavigationSiteRoot
        {
            public const string TemplateName = "_NavigationSiteRoot";
            public const string TemplateID = "{8375C190-5C85-4616-9E14-E8AE5CBAD135}";
        }

        public static class NavigationRoot
        {
            public const string TemplateID = "{93962463-4F5F-45E8-BE15-79F96D0CBB04}";
            public const string FooterConfiguration_FieldId = "{E27B5DB1-CE93-4CB1-BA69-F78556AACAF0}";
            public const string HeaderConfiguration_FieldID = "{95F0787B-70E6-4164-98C0-F9502163D720}";
            public const string ContactUsPage_FieldID = "{0D831D08-90C7-4E31-B880-5A93146B3029}";
            public const string MyLionTrust_FieldID = "{B810A405-73F4-48CC-AF61-778C82531262}";
            public const string MyPreferences_FieldID = "{6F6A49B5-C780-4EDE-9EB4-9EA509204434}";
            public const string SignUpNewsletter_FieldID = "{6EB0248B-457F-491C-8F9E-13AEFE2CDEC8}";
            public const string YouAreViewingLabel_FieldID = "{15999105-600F-43FB-B206-ED93304A98E6}";
            public const string ChangeLabel_FieldID = "{E4A364E6-2E1A-4854-ABD8-6A02ACF832CD}";
            public const string MenuItems_FieldID = "{5D68CC82-EA43-4789-B892-0E49244F68D5}";
            public const string OnboardingConfiguation_FieldId = "{16512055-116D-48B5-936C-ECA678249335}";
        }

        public static class QuickLinks
        {
            public const string QuickLinksList_FieldName = "QuickLinks_QuickLinksList";
            public const string QuickLinksList_FieldId = "{0D827B79-FD2F-46C0-8C0B-6EC87D4BC086}";
        }

        public static class PageTypes
        {
            public const string LearningResourcesTemplateId = "{7B3DCDF5-5735-4FC8-89EB-DCB0F2F72972}";
        }

        public static class TwitterAccount
        {
            public const string TemplateId = "{23084602-8AE7-4B97-96C0-E89784C376D7}";
            public const string TwitterAccountName_FieldId = "{12150827-6E1A-4A38-91A1-70EB00F3E7B0}";
            public const string TwitterLinkGoal_FieldId = "{01E1D852-248A-450D-9A14-AB7EF92DBFE0}";
        }

        public static class OnboardingConfiguration
        {
            public const string PrivateProfileCard_FieldId = "{0D201AF9-3ADA-4859-8B60-C2DA64C5F059}";
            public const string ProfessionalProfileCard_FieldId = "{A2546C2D-E821-46F7-8042-D78F3F1CA8F1}";
        }
    }
}