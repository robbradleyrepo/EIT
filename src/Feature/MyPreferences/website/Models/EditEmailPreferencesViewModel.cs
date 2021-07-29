namespace LionTrust.Feature.MyPreferences.Models
{
    public class EditEmailPreferencesViewModel
    {
        public EditEmailPreferencesViewModel(EmailPreferences emailPreferences)
        {
            EmailPreferences = emailPreferences;
        }
        public EmailPreferences EmailPreferences { get; set; }
    }
}