using LionTrust.Foundation.Contact.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace LionTrust.Feature.MyPreferences.Models
{
    public class EditEmailPreferencesViewModel
    {
        public EditEmailPreferencesViewModel(Context context, IEditEmailPreferences editEmailPreferences)
        {
            if (context.Preferences != null)
            {
                SFProcessList = context.Preferences.SFProcessList;
                IncludeInLTNews = context.Preferences.IncludeInLTNews;
                UnsubscribeAll = context.IsContact;
                IsConsentGivenDateEmpty = context.Preferences.IsConsentGivenDateEmpty;
                IsInstitutionalBulletin = context.Preferences.IsInstitutionalBulletinChecked;
                EmailAddress = context.Preferences.EmailAddress;
            }

            Content = editEmailPreferences;
        }

        public EditEmailPreferencesViewModel()
        {

        }

        [Required]
        public string EmailAddress { get; set; }

        public IEditEmailPreferences Content { get; set; }

        public bool IncludeInLTNews { get; set; }

        public bool UnsubscribeAll { get; set; }

        public bool IsConsentGivenDateEmpty { get; set; }

        public bool IsInstitutionalBulletin { get; set; }

        [Setting, DefaultValue(default(List<SFProcess>))]
        public IList<SFProcess> SFProcessList { get; set; }

        public string ErrorText { get; set; }

       
    }
}