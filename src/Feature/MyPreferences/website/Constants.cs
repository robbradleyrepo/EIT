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
            public const string PreferencesPage_FieldId = "{14DADE68-671B-4478-AFA8-090CD5DE57CD}";
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

        public enum Errors
        {
            UserExists,
            General,
            None
        }
    }
}