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
        public List<SFProcess> SFProcessList { get; set; }

        public EmailPreferences()
        {
            SFProcessList = new List<SFProcess>();
        }

        public void UnsubscribeAll()
        {
            IncludeInLTNews = false;
            Unsubscribe = true;
        }

        public void SubscribeToInsights()
        {
            IncludeInLTNews = false;
            Unsubscribe = false;
        }

        public void SubscribeAll()
        {
            IncludeInLTNews = true;
            Unsubscribe = false;
        }
    }
}
