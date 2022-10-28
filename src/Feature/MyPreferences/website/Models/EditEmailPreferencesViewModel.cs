namespace LionTrust.Feature.MyPreferences.Models
{
    using LionTrust.Foundation.Contact.Models;
    using System.Collections.Generic;

    public class EditEmailPreferencesViewModel
    {
        public EditEmailPreferencesViewModel(Context context, IEditEmailPreferences editEmailPreferences)
        {
            if (context.Preferences != null)
            {
                UnsubscribeAll = context.Preferences.Unsubscribe;
                ShowUnsubscribeTortoise = context.Preferences.TortoiseNewsletter;
                IsConsentGivenDateEmpty = context.Preferences.IsConsentGivenDateEmpty;
            }

            Content = editEmailPreferences;
        }

        public EditEmailPreferencesViewModel()
        {

        }

        public IEditEmailPreferences Content { get; set; }

        public bool UnsubscribeAll { get; set; }

        public bool ShowUnsubscribeTortoise { get; set; }

        public bool UnsubscribeTortoise { get; set; }

        public bool IsConsentGivenDateEmpty { get; set; }

        public string Error { get; set; }

        public List<SFProcess> SFProcessList { get; set; }

        public string DatasourceId { get; set; }

    }
}