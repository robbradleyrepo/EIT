namespace LionTrust.Foundation.Contact.Models
{
    using System.Collections.Generic;

    public class EmailPreferences
    {       
        public string SFEntityId { get; set; }
        public string SFOrgId { get; set; }
        public bool IsContact { get; set; }
        public string SFRandomGUID { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IncludeInLTNews { get; set; }
        public bool UnsubscribeAll { get; set; }
        public bool IsConsentGivenDateEmpty { get; set; }
        public bool IsUkResident { get; set; }
        public bool IsInstitutionalBulletinChecked { get; set; }
        public List<SFProcess> SFProcessList { get; set; }

        public EmailPreferences()
        {
            SFProcessList = new List<SFProcess>();
        }
    }
}
