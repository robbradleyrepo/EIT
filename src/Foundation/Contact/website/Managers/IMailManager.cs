namespace LionTrust.Foundation.Contact.Managers
{
    public interface IMailManager
    {
        void SendEmail(string fromAddress, string fromName, string toAddresses, string subject, string message, bool isHtml);
        void SendEmail(string fromAddress, string fromName, string toAddresses, string ccAddress, string bccAddress, string subject, string message, bool isHtml);
    }
}
