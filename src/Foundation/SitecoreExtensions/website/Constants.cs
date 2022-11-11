using Sitecore.Data;

namespace LionTrust.Foundation.SitecoreExtensions
{
    public static class Constants
    {
        public static class Site
        {
            public static ID Template_ID = new ID("{BB85C5C2-9F87-48CE-8012-AF67CF4F765D}");
        }

        public static class PlaceholderSettings
        {
            public static ID FoundationFolder_ID = new ID("{928A480F-8D07-4CC1-8B73-B46E20AABACC}");
            public static ID FeatureFolder_ID = new ID("{1B59149D-7ED3-4D58-86A9-32EDED8C0368}");
            public static ID LionTrustFolder_ID = new ID("{6CF55936-0B82-4EF5-9DC2-81D8AE0A8DDB}");
            public static ID EITFolder_ID = new ID("{52E0DB23-34DF-41F9-BAB1-A8A15C0CE90B}");
        }

        /// <summary>Name of the Site Cookie.</summary>
        public const string SiteCookieName = "sc_site";
        /// <summary>Name of the Page Site Cookie.</summary>
        public const string PageSiteCookieName = "pagesite";

        public static class SiteNames
        {
            public const string LionTrust = "LionTrust";
            public const string EIT = "EIT";
        }
    }
}