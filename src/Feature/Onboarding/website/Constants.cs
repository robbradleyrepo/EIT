using Sitecore.Data;

namespace LionTrust.Feature.Onboarding
{
    public static class Constants
    {
        public static class Cookies
        {
            public const string CurrentTab_CookieName = "currentTab";
        }

        public static class Analytics
        {
            public const string DefaultAddress_EntityName = "default";
            public const string Addresses_FacetName = "Addresses";
            public const string ProfileCardValue_FieldId = "{85970AB7-22EA-4206-BE86-C0167178860B}";
            public const string ProfileCardValueKey_XmlElementName = "key";
            public const string ProfileCardValueName_XmlAttribute = "name";
            public const string ProfileCardValueValue_XmlAttribute = "value";
        }

        public static class EditPreferencesPage
        {
            public const string TemplateId = "{68ED9154-702A-4D18-A718-4BCC5AC1D5E6}";
        }

        public static class NotFoundPage
        {
            public static ID ItemId = new ID("{0FC62B48-FA17-4728-A923-DC6049B44E93}");
        }

        public enum Tabs
        {
            CountryList,
            CountryGeoIp
        }
    }
}