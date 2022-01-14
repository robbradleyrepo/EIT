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
    }
}
