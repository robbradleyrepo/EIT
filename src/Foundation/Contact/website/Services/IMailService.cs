namespace LionTrust.Foundation.Contact.Services
{
    public interface IMailService
    {
        void SendEmail(string fromAddress, string fromName, string toAddresses, string subject, string message, bool isHtml);
        void SendEmail(string fromAddress, string fromName, string toAddresses, string ccAddress, string bccAddress, string subject, string message, bool isHtml);
    }
}
