namespace LionTrust.Feature.MyPreferences
{
    public static class Constants
    {
        public static class RegisterInvestor
        {
            public const string Title_FieldId = "{DD96AF02-2C1F-42C3-82AB-273BCE9BFD54}";
            public const string Subtitle_FieldId = "{68118BDB-8586-45C8-B3AE-69C285131A5B}";
            public const string Description_FieldId = "{ABAC9452-B35B-4FEA-88D5-8B0846826D46}";
            public const string UserExistsErrorLabel_FieldId = "{EB33C97A-2A88-4E74-9B0B-427398434015}";
            public const string GenericErrorLabel_FieldId = "{E030CE1E-7790-4879-96D2-0C3F45D8DF72}";
            public const string DefaultSFOrganisationId_FieldId = "{2B64A25F-BC2E-4C05-8192-97F2C141D433}";
            public const string CompanyFieldDefaultValue_FieldId = "{E87507FF-AC7B-40E0-B4DE-5D19D8533AA8}";
            public const string ResendEmailSuccessPage_FieldId = "{E7D2B0E3-8266-4B48-8DA2-DFAF29038A85}";
            public const string ResendEmailFailedPage_FieldId = "{98CA7A78-58FC-4713-90A5-1F36BD800800}";
            public const string EditPreferencesPage_FieldId = "{14DADE68-671B-4478-AFA8-090CD5DE57CD}";
            public const string FundDashboardPage_FieldId = "{82ED5593-6B26-406C-A48F-8289F5D915F1}";
            public const string ConfirmationPage_FieldId = "{FF8C897E-34B4-4181-BFB0-BFCE16ABC672}";
            public const string UKEmailTemplate_FieldId = "{C60D363F-8308-4AC9-93A3-1F7E63F222C5}";
            public const string NonUKEmailTemplate_FieldId = "{478C282E-A696-45A5-9FCC-26440C77B2D0}";
            public const string ResendEditPreferencesEmailTemplate_FieldId = "{BC08AE09-D936-4BEE-9B68-421A8A540193}";
        }
        public static class EditEmailPrefTemplate
        {
            public const string FromAddress_FieldId = "{7D7FA98C-6223-4572-BB3C-C7C055125A5A}";
            public const string FromDisplayName_FieldId = "{469A6D86-9FEE-4344-872C-ADD443848237}";
            public const string Subject_FieldId = "{E20E06C6-6506-4B38-8012-53983E7A3D48}";
            public const string Message_FieldId = "{0A8BDBC5-3A31-4B6D-B951-757771FD99A0}";
        }

        public static class EditEmailPreferences
        {
            public const string Title_FieldId = "{AE3B72BB-EBDE-4F47-B046-ED56E78DC358}";
            public const string Subtitle_FieldId = "{781EE4D7-46FF-4D7E-9767-F6F45B6D0055}";
            public const string NewsTitle_FieldId = "{AB693D21-2EBB-4F6D-8198-EF527BEC01B1}";
            public const string NewsSubtitle_FieldId = "{89505D61-D495-4661-A130-1503A29555B0}";
            public const string InstitutionalBulletinTitle_FieldId = "{6D4C1618-7856-4BF4-B0F4-0433228F1999}";
            public const string InstitutionalBulletinSubtitle_FieldId = "{D85E6A06-6437-4BD5-9233-8BBCC464D01F}";
            public const string ProcessListTitle_FieldId = "{5B50EE91-3C3A-42FA-8BD9-19DD18F90913}";
            public const string ProcessListSubtitle_FieldId = "{4C516794-18B3-42C9-AD5B-5D1FEB7146A0}";
            public const string CheckboxInstructionText_FieldId = "{B02D0482-E724-4FD7-9122-194D11C7DE38}";
            public const string SuccessPage_FieldId = "{71F7C4F1-2285-48C3-A4EC-98E732B394E2}";
            public const string FailedPage_FieldId = "{5D11C14A-6325-4D99-A30C-C9F568264C66}";
            public const string PrivacyPolicyText_FieldId = "{C7E53F1A-D525-4576-9C47-B8C651A95047}";
            public const string UnsubscribeAllText_FieldId = "{184A144B-A5E5-4349-A5DC-BB61C5C0FB75}";
            public const string GlobalSelectAllCheckboxText_FieldId = "{2B444A22-FB51-403E-986D-2097769CE386}";
            public const string GenericError_FieldId = "{B00A7599-FD4A-493A-899C-16095ECDE601}";
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
                    public const string FundDashboardLinkToken = "{FundDashboardLink}";
                    public const string SiteURLToken = "{SiteURL}";
                }
            }
        }

        public enum Errors
        {
            UserExists,
            General,
            None
        }
    }
}