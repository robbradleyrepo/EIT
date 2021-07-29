namespace LionTrust.Feature.MyPreferences.Models
{
    public class ResendEmailPrefEmailDetails
    {        
        public string FromAddress { get; set; }
        public string FromDisplyName { get; set; }
        public string ToAddresses { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}