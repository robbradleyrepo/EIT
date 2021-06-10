namespace LionTrust.Foundation.SitecoreForms.Models
{
    public class SendEmailTemplate
    {
        public string Subject { get; set; }
        public string From { get; set; }
        public string FromDisplyName { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Message { get; set; }
    }
}
