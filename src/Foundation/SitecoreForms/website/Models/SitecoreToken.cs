namespace LionTrust.Foundation.SitecoreForms.Models
{
    public static class SitecoreToken
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
}