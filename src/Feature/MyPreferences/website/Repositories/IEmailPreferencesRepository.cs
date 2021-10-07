namespace LionTrust.Feature.MyPreferences.Repositories
{
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Foundation.Contact.Models;
    using System.Collections.Generic;

    public interface IEmailPreferencesRepository
    {
        EmailPreferences GetEmailPreferences(Context context);
        bool SaveEmailPreferneces(Context context);
        RegisterdUserWithEmailDetails SaveNonProfUserAsSFLead(NonProfessionalUser nonProfessionalUser, IEditEmailPrefTemplate emailTemplate, string preferencesUrl, string fundDashboardUrl);
        RegisterdUserWithEmailDetails SaveProfUserAsSFContact(ProfessionalUser professionalUser, IEditEmailPrefTemplate emailTemplate, string preferencesUrl, string fundDashboardUrl);
        SFCountryListViewModel GetCountryListFromSF(bool isFromContact, string defaultOptionText);
        ResendEmailPrefEmailDetails GetEmailDetailsForResendEmailPrefLink(string email, bool isContact, IEditEmailPrefTemplate emailTemplate, string preferencesUrl, string fundDashboardUrl);
        IEnumerable<SFProcess> GetSFProcessList();
    }
}
