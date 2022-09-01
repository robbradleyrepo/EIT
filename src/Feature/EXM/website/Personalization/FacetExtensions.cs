namespace LionTrust.Feature.EXM.Personalization
{
    using FuseIT.Sitecore.Personalization.Facets;

    public static class FacetExtensions
    {
        public static string GetEmailPreferencesId(S4SInfo info)
        {
            var sfEntityId = GetFieldValue(info, Foundation.Contact.Constants.SF_IdField);
            var randomGuid = GetFieldValue(info, Foundation.Contact.Constants.SF_GUIDForEmailPref);

            var emailPreferencesId = $"{randomGuid}_{sfEntityId}";
            return emailPreferencesId;
        }

        public static string GetOwnerTitle(S4SInfo info)
        {
            return GetFieldValue(info, Foundation.Contact.Constants.SF_Owner_TitleField);
        }

        public static string GetOwnerName(S4SInfo info)
        {
            return GetFieldValue(info, Foundation.Contact.Constants.SF_Owner_NameField);
        }

        public static string GetOwnerEmail(S4SInfo info)
        {
            return GetFieldValue(info, Foundation.Contact.Constants.SF_Owner_EmailField);
        }

        public static string GetOwnerPhone(S4SInfo info)
        {
            return GetFieldValue(info, Foundation.Contact.Constants.SF_Owner_PhoneField);
        }

        public static string GetOwnerRegion(S4SInfo info)
        {
            return GetFieldValue(info, Foundation.Contact.Constants.SF_Owner_RegionField);
        }

        private static string GetFieldValue(S4SInfo info, string key)
        {
            return info.Fields.ContainsKey(key) ? info.Fields[key] : null;
        }
    }
}
