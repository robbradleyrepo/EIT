namespace LionTrust.Foundation.Contact.Models.EditEmailPreferences
{
    public class RegisterdUserWithEmailDetails
    {
        public bool IsUserExists { get; set; }
        public string FromAddress { get; set; }
        public string FromDisplyName { get; set; }
        public string ToAddresses { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}