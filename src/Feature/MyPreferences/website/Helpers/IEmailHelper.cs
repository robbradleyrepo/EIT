namespace LionTrust.Feature.MyPreferences.Helpers
{
    public interface IEmailHelper
    {
        /// <summary>
        /// Generate email message body
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fullName"></param>
        /// <param name="editEmailPrefLink"></param>
        /// <param name="fundDashboardLink"></param>
        /// <returns></returns>
        string GenerateEmailMessageBody(string message, string fullName, string editEmailPrefLink, string fundDashboardLink);
    }
}