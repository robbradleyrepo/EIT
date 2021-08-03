using LionTrust.Foundation.Contact.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace LionTrust.Feature.MyPreferences.Models
{
    public class EditEmailPreferencesViewModel
    {
        public EditEmailPreferencesViewModel(EmailPreferences emailPreferences, IEditEmailPreferences editEmailPreferences)
        {
            SFEntityId = emailPreferences.SFEntityId;
            SFRandomGUID = emailPreferences.SFRandomGUID;
            SFProcessList = emailPreferences.SFProcessList;
            IncludeInLTNews = emailPreferences.IncludeInLTNews;
            UnsubscribeAll = emailPreferences.IsContact;
            IsConsentGivenDateEmpty = emailPreferences.IsConsentGivenDateEmpty;
            IsUkResident = emailPreferences.IsUkResident;
            IsInstitutionalBulletin = emailPreferences.IsInstitutionalBulletinChecked;
            EmailAddress = emailPreferences.EmailAddress;

            Content = editEmailPreferences;
        }

        public EditEmailPreferencesViewModel()
        {

        }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string SFRandomGUID { get; set; }

        [Required]
        public string SFEntityId { get; set; }

        public IEditEmailPreferences Content { get; set; }

        public bool IncludeInLTNews { get; set; }

        public bool UnsubscribeAll { get; set; }

        public bool IsConsentGivenDateEmpty { get; set; }

        public bool IsUkResident { get; set; }

        public bool IsInstitutionalBulletin { get; set; }

        [Setting, DefaultValue(default(List<SFProcess>))]
        public IList<SFProcess> SFProcessList { get; set; }

        public string ErrorText { get; set; }

       
    }
}