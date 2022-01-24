namespace LionTrust.Feature.EXM.Personalization
{
    using LionTrust.Foundation.Contact.Services;
    using Sitecore.XConnect.Collection.Model;

    public static class FacetExtensions
    {
        public static string GetEmailPreferencesId(EmailAddressList emailAddressList)
        {
            var preferredEmail = emailAddressList?.PreferredEmail;

            if (!string.IsNullOrWhiteSpace(preferredEmail?.SmtpAddress))
            {
                var email = preferredEmail.SmtpAddress;

                var sfEntityUtility = new SFEntityUtility();
                var emailPreferencesId = sfEntityUtility.GetEmailPreferencesIdByEmail(email);

                return emailPreferencesId;
            }

            return null;
        }

        public static string GetOwnerTitle(EmailAddressList emailAddressList)
        {
            var preferredEmail = emailAddressList?.PreferredEmail;

            if (!string.IsNullOrWhiteSpace(preferredEmail?.SmtpAddress))
            {
                var email = preferredEmail.SmtpAddress;

                var sfEntityUtility = new SFEntityUtility();
                var ownerTitle = sfEntityUtility.GetOwnerTitleByEmail(email);

                return ownerTitle;
            }

            return null;
        }

        public static string GetOwnerName(EmailAddressList emailAddressList)
        {
            var preferredEmail = emailAddressList?.PreferredEmail;

            if (!string.IsNullOrWhiteSpace(preferredEmail?.SmtpAddress))
            {
                var email = preferredEmail.SmtpAddress;

                var sfEntityUtility = new SFEntityUtility();
                var ownerName = sfEntityUtility.GetOwnerNameByEmail(email);

                return ownerName;
            }

            return null;
        }

        public static string GetOwnerEmail(EmailAddressList emailAddressList)
        {
            var preferredEmail = emailAddressList?.PreferredEmail;

            if (!string.IsNullOrWhiteSpace(preferredEmail?.SmtpAddress))
            {
                var email = preferredEmail.SmtpAddress;

                var sfEntityUtility = new SFEntityUtility();
                var ownerEmail = sfEntityUtility.GetOwnerEmailByEmail(email);

                return ownerEmail;
            }

            return null;
        }

        public static string GetOwnerPhone(EmailAddressList emailAddressList)
        {
            var preferredEmail = emailAddressList?.PreferredEmail;

            if (!string.IsNullOrWhiteSpace(preferredEmail?.SmtpAddress))
            {
                var email = preferredEmail.SmtpAddress;

                var sfEntityUtility = new SFEntityUtility();
                var ownerPhone = sfEntityUtility.GetOwnerPhoneByEmail(email);

                return ownerPhone;
            }

            return null;
        }

        public static string GetOwnerRegion(EmailAddressList emailAddressList)
        {
            var preferredEmail = emailAddressList?.PreferredEmail;

            if (!string.IsNullOrWhiteSpace(preferredEmail?.SmtpAddress))
            {
                var email = preferredEmail.SmtpAddress;

                var sfEntityUtility = new SFEntityUtility();
                var ownerRegion = sfEntityUtility.GetOwnerRegionByEmail(email);

                return ownerRegion;
            }

            return null;
        }
    }
}
