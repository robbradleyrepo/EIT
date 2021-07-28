namespace LionTrust.Foundation.Contact.Repositories
{
    using LionTrust.Foundation.Contact.Models.EditEmailPreferences;

    public interface IEmailPreferencesRepository
    {
        EmailPreferences GetEmailPreferences(string sfEntityId, string sfRandomGuid, bool isContact);
        bool SaveEmailPreferneces(EmailPreferences emailPreferences);
        RegisterdUserWithEmailDetails SaveNonProfUserAsSFLead(NonProfessionalUser nonProfUserViewModel);
        RegisterdUserWithEmailDetails SaveProfUserAsSFContact(ProfessionalUser profUserViewModel);
        SFCountryListViewModel GetCountryListFromSF(bool isFromContact, string defaultOptionText);
        string GetRedirectUrlAfterEditing(bool isSuccess);
        ResendEmailPrefEmailDetails GetEmailDetailsForResendEmailPrefLink(string email, bool isContact);
        ResendEmailPrefLinkUIDetails GetUIDetailsForResendEmailPrefEmail(bool isSuccess);
    }
}
