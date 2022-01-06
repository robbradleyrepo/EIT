namespace LionTrust.Foundation.Contact.Models
{
    public class Context
    {
        public bool IsContact { get; set; }
        public string SFRandomGUID { get; set; }
        public string SFEntityId { get; set; }

        public EmailPreferences Preferences { get; set; } = new EmailPreferences();
    }
}
