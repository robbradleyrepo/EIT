namespace LionTrust.Foundation.Contact.Models
{
    public class RegisterdUserReturnViewModel
    {
        public bool IsUserExists { get; set; }
        public string FullName { get; set; }
        public string  EditEmailPrefLink { get; set; }
        public string EmailAddress { get; set; }
    }
}
