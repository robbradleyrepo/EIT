namespace LionTrust.Foundation.Contact.Models
{
    using System.Collections.Generic;

    public class EmailPreferences
    {       
        public string SFOrgId { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IncludeInLTNews { get; set; }
        public bool Unsubscribe { get; set; }
        public bool IsConsentGivenDateEmpty { get; set; }
        public bool IsUkResident { get; set; }
        public bool IsInstitutionalBulletinChecked { get; set; }
        public List<SFProcess> SFProcessList { get; set; }

        public EmailPreferences()
        {
            SFProcessList = new List<SFProcess>();
        }

        public void UnsubscribeAll()
        {
            IsInstitutionalBulletinChecked = false;
            IncludeInLTNews = false;
            Unsubscribe = true;
        }

        public void SubscribeAll()
        {
            IsInstitutionalBulletinChecked = true;
            IncludeInLTNews = true;
            Unsubscribe = false;
        }
    }
}
