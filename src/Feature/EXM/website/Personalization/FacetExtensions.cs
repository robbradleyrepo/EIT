namespace LionTrust.Feature.EXM.Personalization
{
    using FuseIT.Sitecore.Personalization.Facets;
    using LionTrust.Feature.EXM.Helpers.Implementations;

    public static class FacetExtensions
    {
        public static string GetEmailPreferencesId(S4SInfo info)
        {
            var sfEntityId = SFEntityHelper.GetFieldValue(info, Foundation.Contact.Constants.SF_IdField);
            var randomGuid = SFEntityHelper.GetFieldValue(info, Foundation.Contact.Constants.SF_GUIDForEmailPref);

            if (string.IsNullOrEmpty(sfEntityId) || string.IsNullOrEmpty(randomGuid)) 
            {
                return null;
            }

            var emailPreferencesId = $"{randomGuid}_{sfEntityId}";
            return emailPreferencesId;
        }

        public static string GetOwnerJob(S4SInfo info)
        {
            return SFEntityHelper.GetFieldValue(info, Foundation.Contact.Constants.SF_Owner_TitleField, Foundation.Contact.Constants.SF_User_TitleField);
        }

        public static string GetOwnerName(S4SInfo info)
        {
            return SFEntityHelper.GetFieldValue(info, Foundation.Contact.Constants.SF_Owner_NameField, Foundation.Contact.Constants.SF_User_NameField);
        }

        public static string GetOwnerEmail(S4SInfo info)
        {
            return SFEntityHelper.GetFieldValue(info, Foundation.Contact.Constants.SF_Owner_EmailField, Foundation.Contact.Constants.SF_User_EmailField);
        }

        public static string GetOwnerPhone(S4SInfo info)
        {
            return SFEntityHelper.GetFieldValue(info, Foundation.Contact.Constants.SF_Owner_PhoneField, Foundation.Contact.Constants.SF_User_PhoneField);
        }

        public static string GetOwnerRegion(S4SInfo info)
        {
            return SFEntityHelper.GetFieldValue(info, Foundation.Contact.Constants.SF_Owner_RegionField, Foundation.Contact.Constants.SF_User_RegionField);
        }
    }
}
