using Sitecore.Abstractions;
using System.Text.RegularExpressions;

namespace LionTrust.Feature.MyPreferences.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private const string RelativeImageRegex = " src=\"[/]?-/media/";
        private const string ImageSrc = " src=\"http://{0}/-/media/";

        private readonly BaseFactory _factory;

        public EmailHelper(BaseFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Generate email message body
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fullName"></param>
        /// <param name="editEmailPrefLink"></param>
        /// <param name="fundDashboardLink"></param>
        /// <returns></returns>
        public string GenerateEmailMessageBody(string message, string fullName, string editEmailPrefLink, string fundDashboardLink)
        {
            var hostName = _factory.GetSite(Foundation.Indexing.Constants.SiteName).HostName;

            //Generate email body
            var emailMessageBody = message;
            emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.FullNameToken, fullName);
            emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.EditPrefLinkToken, editEmailPrefLink);
            emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.FundDashboardLinkToken, fundDashboardLink);
            emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.SiteURLToken, string.Format("https://{0}", hostName));
            emailMessageBody = Regex.Replace(emailMessageBody, RelativeImageRegex, string.Format(ImageSrc, hostName));

            return emailMessageBody;
        }
    }
}