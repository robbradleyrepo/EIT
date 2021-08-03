namespace LionTrust.Feature.MyPreferences.Repositories
{
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Foundation.Contact.Models;

    public interface IEmailPreferencesRepository
    {
        EmailPreferences GetEmailPreferences(string sfEntityId, string sfRandomGuid, bool isContact);
        bool SaveEmailPreferneces(EmailPreferences emailPreferences);
        RegisterdUserWithEmailDetails SaveNonProfUserAsSFLead(NonProfessionalUser nonProfessionalUser, IEditEmailPrefTemplate emailTemplate, string preferencesUrl);
        RegisterdUserWithEmailDetails SaveProfUserAsSFContact(ProfessionalUser professionalUser, IEditEmailPrefTemplate emailTemplate, string preferencesUrl);
        SFCountryListViewModel GetCountryListFromSF(bool isFromContact, string defaultOptionText);
        ResendEmailPrefEmailDetails GetEmailDetailsForResendEmailPrefLink(string email, bool isContact, IEditEmailPrefTemplate emailTemplate, string preferencesUrl);
    }
}
