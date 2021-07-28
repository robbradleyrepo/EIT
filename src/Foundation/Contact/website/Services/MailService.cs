namespace LionTrust.Foundation.Contact.Services
{
    using Sitecore;
    using System.Net.Mail;

    public class MailService : IMailService
    {
        public void SendEmail(string fromAddress, string fromName, string toAddresses, string subject, string message, bool isHtml)
        {
            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(fromAddress, fromName);
            var toAddressList = toAddresses.Split(';');
            foreach (var addressItem in toAddressList)
            {
                mailMessage.To.Add(new MailAddress(addressItem));
            }
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = isHtml;
            mailMessage.Body = message;

            MainUtil.SendMail(mailMessage);
        }

        /// <summary>
        /// Send email functionality with cc and bcc options
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="fromName"></param>
        /// <param name="toAddresses"></param>
        /// <param name="ccAddresses"></param>
        /// <param name="bccAddresses"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="isHtml"></param>
        public void SendEmail(string fromAddress, string fromName, string toAddresses, string ccAddresses, string bccAddresses, string subject, string message, bool isHtml)
        {
            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(fromAddress, fromName);

            var toAddressList = toAddresses.Split(',');
            foreach (var addressItem in toAddressList)
            {
                if (!string.IsNullOrEmpty(addressItem))
                {
                    mailMessage.To.Add(new MailAddress(addressItem));
                }
            }

            var ccAddressList = ccAddresses.Split(',');
            foreach (var addressItem in ccAddressList)
            {
                if (!string.IsNullOrEmpty(addressItem))
                {
                    mailMessage.CC.Add(new MailAddress(addressItem));
                }
            }

            var bccAddressList = bccAddresses.Split(',');
            foreach (var addressItem in bccAddressList)
            {
                if (!string.IsNullOrEmpty(addressItem))
                {
                    mailMessage.Bcc.Add(new MailAddress(addressItem));
                }
            }

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = isHtml;
            mailMessage.Body = message;
            MainUtil.SendMail(mailMessage);
        }

    }
}
