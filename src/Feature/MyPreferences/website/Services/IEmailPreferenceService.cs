namespace LionTrust.Feature.MyPreferences.Services
{
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Foundation.Contact.Models;
    using System.Collections.Generic;

    public interface IEmailPreferencesService
    {
        /// <summary>
        /// Save email preferences to Salesforce
        /// </summary>
        /// <param name="emailPreferenceViewModel"></param>
        /// <returns></returns>
        bool SaveEmailPreferences(Context context);

        /// <summary>
        /// Save non professional investor as a Salesforce Lead
        /// </summary>
        /// <param name="nonProfUserViewModel"></param>
        /// <returns></returns>
        RegisterdUserWithEmailDetails SaveNonProfUserAsSFLead(NonProfessionalUser nonProfessionalUser, IEditEmailPrefTemplate emailTemplate, string preferencesUrl, string fundDashboardUrl);

        /// <summary>
        /// Save professional investors as a Salesforce Contact
        /// </summary>
        /// <param name="nonProfUserViewModel"></param>
        /// <returns></returns>
        RegisterdUserWithEmailDetails SaveProfUserAsSFContact(ProfessionalUser professionalUser, IEditEmailPrefTemplate emailTemplate, string preferencesUrl, string fundDashboardUrl);

        /// <summary>
        /// Resend edit email pref link
        /// </summary>
        /// <returns></returns>
        bool ResendEditEmailPrefLink(string email, bool IsContact, IEditEmailPrefTemplate emailTemplate, string preferencesUrl, string fundDashboardUrl);

        /// <summary>
        /// Send automated welcome emails
        /// </summary>
        /// <param name="registerInvestor"></param>
        /// <param name="settings"></param>
        void SendAutomatedWelcomeEmails(IRegisterInvestor registerInvestor, IAutomatedWelcomeSettings settings);

        IEnumerable<SFProcess> GetSFProcessList();
    }
}