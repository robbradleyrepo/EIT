using System;

namespace LionTrust.Feature.MyPreferences
{
    public static class Constants
    {
        public static class RegisterInvestor
        {
            public static Guid Item_ID = new Guid("{15518891-53BA-4B83-93D5-F3BAC21D3344}");

            public static class Content
            {
                public const string IntroductionText_FieldId = "{ABAC9452-B35B-4FEA-88D5-8B0846826D46}";
                public const string UserExistsErrorLabel_FieldId = "{EB33C97A-2A88-4E74-9B0B-427398434015}";
                public const string GenericErrorLabel_FieldId = "{E030CE1E-7790-4879-96D2-0C3F45D8DF72}";
                public const string DefaultSFOrganisationId_FieldId = "{2B64A25F-BC2E-4C05-8192-97F2C141D433}";
                public const string CompanyFieldDefaultValue_FieldId = "{E87507FF-AC7B-40E0-B4DE-5D19D8533AA8}";
                public const string SubmitCTAText_FieldId = "{44EE3542-38DF-4C10-97A8-14636BF87BA6}";
            }

            public static class AboutYou
            {
                public const string AboutYouTitle_FieldId = "{4A7FFD21-99B4-4BC8-B34F-542674B0ED63}";
                public const string YourRoleLabel_FieldId = "{EBE943A0-FCA3-4F1B-A906-87974C1EB2B4}";
                public const string YourCountryLabel_FieldId = "{F72C7721-46AD-4A5D-BB73-0AE4FBD10D04}";
                public const string NotCorrectLabel_FieldId = "{6F14DBF5-E3D3-4AC1-B98E-B266C3C8C4F8}";
                public const string ChangeCTAText_FieldId = "{B26E0103-149F-4716-9BD1-0847B84D0A65}";
            }

            public static class ContentPreferences
            {
                public const string ContentPreferencesTitle_FieldId = "{AA6A40D1-AF9A-43F0-AF77-A3EB780D2229}";
                public const string ContentPreferencesSubtitle_FieldId = "{CAEE178B-FB59-429A-97CC-E5473EEACC72}";
            }

            public static class EmailPreferences
            {
                public const string EmailPreferencesTitle_FieldId = "{ED187AD0-F4C2-451A-BEAD-653A06B02FC5}";
                public const string EmailPreferencesSubtitle_FieldId = "{327759B0-E24D-4E68-9EDD-1972D27855BC}";
                public const string SubscribeText_FieldId = "{40F33FAF-CF8C-4888-9CC0-95333E37510B}";
                public const string FirstNameLabel_FieldId = "{496ABD78-9175-44D2-A695-2FEB9A3B7AE9}";
                public const string LastNameLabel_FieldId = "{CE4BA61A-2B31-4128-A7F9-EA94091E9251}";
                public const string EmailAddressLabel_FieldId = "{9850F151-5E9B-4350-ACF6-A3F56EA2627E}";
                public const string FCALabel_FieldId = "{D358C951-AD75-42DC-AF8A-76D220CBEA05}";
                public const string CompanyNameLabel_FieldId = "{2FC980A9-4F18-451D-8270-67424DA66BC0}";
                public const string OptOutText_FieldId = "{A8C04620-1E7F-4FBD-BD7B-23ACF7D474AE}";
            }

            public static class Pages
            {
                public const string ResendEmailSuccessPage_FieldId = "{E7D2B0E3-8266-4B48-8DA2-DFAF29038A85}";
                public const string ResendEmailFailedPage_FieldId = "{98CA7A78-58FC-4713-90A5-1F36BD800800}";
                public const string EditPreferencesPage_FieldId = "{14DADE68-671B-4478-AFA8-090CD5DE57CD}";
                public const string FundDashboardPage_FieldId = "{82ED5593-6B26-406C-A48F-8289F5D915F1}";
                public const string ConfirmationPage_FieldId = "{FF8C897E-34B4-4181-BFB0-BFCE16ABC672}";
            }

            public static class Emails
            {
                public const string UKEmailTemplate_FieldId = "{C60D363F-8308-4AC9-93A3-1F7E63F222C5}";
                public const string NonUKEmailTemplate_FieldId = "{478C282E-A696-45A5-9FCC-26440C77B2D0}";
                public const string ResendEditPreferencesEmailTemplate_FieldId = "{BC08AE09-D936-4BEE-9B68-421A8A540193}";
            }

            public static class RetrievePreferences
            {
                public const string RetrievePreferencesGoal_FieldId = "{4C1DF9CD-198D-45E5-9092-A5E21BDF5A25}";
                public const string BannerTitle_FieldId = "{EB8323F3-80D2-40F8-915A-EC5771D8A940}";
                public const string BannerCTAText = "{25568FA2-E0DC-40BC-B0C7-63FBAFC8E30B}";
                public const string IntroductionText = "{66D371DB-D7AC-42A0-8A6C-A932C85DB406}";
                public const string EmailLabel = "{A192BC7F-6B60-4555-853F-FFAD8677F29B}";
                public const string CTAText = "{79ED1E30-6DF9-4B71-8DBA-7E98851C8040}";
                public const string EmailNotRecognized = "{A86DB8CA-97F4-4D46-9357-255F92D04CA3}";
            }
        }

        public static class FundAccordionList
        {
            public const string TeamFundsTitle_FieldId = "{10ACE34C-BEF0-4890-A325-2CADE743F274}";
            public const string FollowTeamTitle_FieldId = "{F09282FE-40F7-4427-A26A-412B540F1E41}";
        }

        public static class PrivacyNotice
        {
            public const string Title_FieldId = "{987D00B3-C658-49D7-ABAE-7BCE3C9CF273}";
            public const string Text_FieldId = "{ACA1E97A-349C-46AF-8030-A187E4228C42}";
        }
        public static class EditEmailPrefTemplate
        {
            public const string FromAddress_FieldId = "{7D7FA98C-6223-4572-BB3C-C7C055125A5A}";
            public const string FromDisplayName_FieldId = "{469A6D86-9FEE-4344-872C-ADD443848237}";
            public const string Subject_FieldId = "{E20E06C6-6506-4B38-8012-53983E7A3D48}";
            public const string Message_FieldId = "{0A8BDBC5-3A31-4B6D-B951-757771FD99A0}";
        }
        
        public static class EditPreferencesPage
        {
            public const string TemplateId = "{68ED9154-702A-4D18-A718-4BCC5AC1D5E6}";
        }

        public static class EditPreferences
        {
            public const string Title_FieldId = "{AE3B72BB-EBDE-4F47-B046-ED56E78DC358}";
            public const string InstitutionalBulletinLabel_FieldId = "{6D4C1618-7856-4BF4-B0F4-0433228F1999}";
            public const string InstitutionalBulletinText_FieldId = "{D85E6A06-6437-4BD5-9233-8BBCC464D01F}";
            public const string UnsubscribeAllLabel_FieldId = "{B1204568-EE4B-4DB0-8447-1E18CB3E9291}";
            public const string UnsubscribeAllText_FieldId = "{184A144B-A5E5-4349-A5DC-BB61C5C0FB75}";
            public const string SubmitCTAText_FieldId = "{6AFBB9DE-8C3E-49E1-A38E-78124BC073C1}";
            public const string GenericError_FieldId = "{B00A7599-FD4A-493A-899C-16095ECDE601}";
            public const string SuccessPage_FieldId = "{71F7C4F1-2285-48C3-A4EC-98E732B394E2}";
            public const string SubscribeGoal_FieldId = "{A8C59081-6C37-45DD-9BCF-AB882E8B90C1}";
            public const string UnsubscribeGoal_FieldId = "{47EAB133-5A65-4454-98A3-43911EF21464}";
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
                public static class EmailTokens
                {
                    public const string FullNameToken = "{FullName}";
                    public const string EditPrefLinkToken = "{EditPrefLink}";
                    public const string FundDashboardLinkToken = "{FundDashboardLink}";
                    public const string SiteURLToken = "{SiteURL}";
                }
            }
        }

        public static class AutomatedWelcomeSettings
        {
            public const string Enabled_FieldID = "{A1D79CD8-7DC4-4EDD-9C5B-D186A3A26359}";

            public const string FromAddress_FieldID = "{60B81BA0-0A04-4F73-8131-22330EDD9FFF}";
            public const string FromDisplayName_FieldID = "{545B6376-0BF8-49C0-8B0C-5B6D64A92DFB}";
            public const string ToAddress_FieldID = "{8B08B1B5-BCBF-4BD8-A1E0-12D6A907C055}";
            public const string Subject_FieldID = "{D9A994F4-3102-48ED-9C42-E9B2730D5002}";
        }

        public enum Errors
        {
            UserExists,
            General,
            None,
            EmailNotRecognized
        }
    }
}