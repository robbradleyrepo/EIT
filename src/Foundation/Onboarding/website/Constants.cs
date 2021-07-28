namespace LionTrust.Foundation.Onboarding
{
    public static class Constants
    {
        public static class OnboardingConfiguration
        {
            public const string Logo_FieldId = "{B2EFA293-4240-4439-997F-DDAC10179067}";
            public const string Text_FieldId = "{44AFFB73-0BB2-4312-8968-39D3737453E4}";
            public const string PrivateProfileCard_FieldId = "{0D201AF9-3ADA-4859-8B60-C2DA64C5F059}";
            public const string ProfessionalProfileCard_FieldId = "{A2546C2D-E821-46F7-8042-D78F3F1CA8F1}";
            public const string Profile_FieldId = "{0E092A53-4282-4D27-BB59-8FEFD1E28196}";
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

        public enum InvestorType
        {
            Private,
            Professional
        }
    }
}
