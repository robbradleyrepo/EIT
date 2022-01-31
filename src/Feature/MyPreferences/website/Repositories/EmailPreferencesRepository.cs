namespace LionTrust.Feature.MyPreferences.Repositories
{
    using LionTrust.Foundation.Contact.Models;
    using LionTrust.Feature.MyPreferences.Models;
    using System;
    using System.Web;
    using LionTrust.Foundation.Contact.Services;
    using System.Collections.Generic;

    public class EmailPreferencesRepository : IEmailPreferencesRepository
    {
        private readonly IApplicationCacheRepository _applicationCacheRepository;
        private const string SFContactCountryListCacheKey = "salesforce-contact-country-list";
        private const string SFLeadCountryListCacheKey = "salesforce-lead-country-list";
        private const string RelativeImagePath = " src=\"/-/media/";
        private const string ImageSrc = " src=\"http://{0}/-/media/";

        public EmailPreferencesRepository(IApplicationCacheRepository applicationCacheRepository)
        {
            _applicationCacheRepository = applicationCacheRepository;
        }
        public EmailPreferences GetEmailPreferences(Context context)
        {
            var sfEntityUtility = new SFEntityUtility();
            return sfEntityUtility.GetSFEmailPreferences(context);
        }

        public bool SaveEmailPreferneces(Context context)
        {
            var sfEntityUtility = new SFEntityUtility();
            return sfEntityUtility.SaveEmailPreferences(context);
        }

        public RegisterdUserWithEmailDetails SaveNonProfUserAsSFLead(NonProfessionalUser nonProfessionalUser, IEditEmailPrefTemplate emailTemplate, string preferencesUrl, string fundDashboardUrl)
        {
            var sfEntityUtility = new SFEntityUtility();
            var savedUserEmailDetails = sfEntityUtility.SaveNonProfUserAsSFLead(nonProfessionalUser, preferencesUrl, fundDashboardUrl);
            if (savedUserEmailDetails != null)
            {
                if (savedUserEmailDetails.IsUserExists)
                {
                    return new RegisterdUserWithEmailDetails { IsUserExists = savedUserEmailDetails.IsUserExists };
                }

                //Generate email body
                var emailMessageBody = emailTemplate.Message;
                emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.FullNameToken, savedUserEmailDetails.FullName);
                emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.EditPrefLinkToken, savedUserEmailDetails.EditEmailPrefLink);
                emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.FundDashboardLinkToken, savedUserEmailDetails.FundDashboardLink);
                emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.SiteURLToken, string.Format("https://{0}", HttpContext.Current.Request.Url.Host));
                emailMessageBody = emailMessageBody.Replace(RelativeImagePath, string.Format(ImageSrc, HttpContext.Current.Request.Url.Host));

                var returnObj = new RegisterdUserWithEmailDetails
                {
                    IsUserExists = savedUserEmailDetails.IsUserExists,
                    FromAddress = emailTemplate.FromAddress,
                    FromDisplyName = emailTemplate.FromDisplayName,
                    ToAddresses = savedUserEmailDetails.EmailAddress,
                    Subject = emailTemplate.Subject,
                    Message = emailMessageBody
                };

                return returnObj;
            }

            return null;
        }

        public RegisterdUserWithEmailDetails SaveProfUserAsSFContact(ProfessionalUser professionalUser, IEditEmailPrefTemplate emailTemplate, string preferencesUrl, string fundDashboardUrl)
        {
            var sfEntityUtility = new SFEntityUtility();
            var savedUserEmailDetails = sfEntityUtility.SaveProfUserAsSFContact(professionalUser, preferencesUrl, fundDashboardUrl);
            if (savedUserEmailDetails != null)
            {
                if (savedUserEmailDetails.IsUserExists)
                {
                    return new RegisterdUserWithEmailDetails { IsUserExists = savedUserEmailDetails.IsUserExists };
                }

                //Generate email body
                var emailMessageBody = emailTemplate.Message;
                emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.FullNameToken, savedUserEmailDetails.FullName);
                emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.EditPrefLinkToken, savedUserEmailDetails.EditEmailPrefLink);
                emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.FundDashboardLinkToken, savedUserEmailDetails.FundDashboardLink);
                emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.SiteURLToken, string.Format("https://{0}", HttpContext.Current.Request.Url.Host));
                emailMessageBody = emailMessageBody.Replace(RelativeImagePath, string.Format(ImageSrc, HttpContext.Current.Request.Url.Host));

                var returnObj = new RegisterdUserWithEmailDetails
                {
                    IsUserExists = savedUserEmailDetails.IsUserExists,
                    FromAddress = emailTemplate.FromAddress,
                    FromDisplyName = emailTemplate.FromDisplayName,
                    ToAddresses = savedUserEmailDetails.EmailAddress,
                    Subject = emailTemplate.Subject,
                    Message = emailMessageBody
                };

                return returnObj;

            }

            return null;
        }

        public SFCountryListViewModel GetCountryListFromSF(bool isFromContact, string defaultOptionText)
        {
            var sfEntityUtility = new SFEntityUtility();
            var cacheKey = (isFromContact) ? SFContactCountryListCacheKey : SFLeadCountryListCacheKey;
            var countryListViewModel = _applicationCacheRepository.Read<SFCountryListViewModel>(cacheKey);
            //If not in cache, retrieve directly from SF
            if (countryListViewModel == default(SFCountryListViewModel))
            {
                countryListViewModel = sfEntityUtility.GetCountryListFromSF(isFromContact, defaultOptionText);
                _applicationCacheRepository.Write(cacheKey, countryListViewModel, new TimeSpan(6, 0, 0));
            }

            return countryListViewModel;
        }

        public ResendEmailPrefEmailDetails GetEmailDetailsForResendEmailPrefLink(string email, bool isContact, IEditEmailPrefTemplate emailTemplate, string preferencesUrl, string fundDashboardUrl)
        {
            var sfEntityUtility = new SFEntityUtility();
            var userDetails = sfEntityUtility.GetEmailDetailsForResendEmailPrefLink(email, isContact, preferencesUrl, fundDashboardUrl);

            if (userDetails != null)
            {

                //Generate email body
                var emailMessageBody = emailTemplate.Message;
                emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.FullNameToken, userDetails.FullName);
                emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.EditPrefLinkToken, userDetails.EditEmailPrefLink);
                emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.FundDashboardLinkToken, userDetails.FundDashboardLink);
                emailMessageBody = emailMessageBody.Replace(Constants.SitecoreTokens.RegisterUserProcess.EmailTokens.SiteURLToken, string.Format("https://{0}", HttpContext.Current.Request.Url.Host));

                var returnObj = new ResendEmailPrefEmailDetails
                {
                    FromAddress = emailTemplate.FromAddress,
                    FromDisplyName = emailTemplate.FromDisplayName,
                    ToAddresses = email,
                    Subject = emailTemplate.Subject,
                    Message = emailMessageBody
                };

                return returnObj;
            }

            return null;
        }

        public IEnumerable<SFProcess> GetSFProcessList()
        {
            var sfEntityUtility = new SFEntityUtility();
            return sfEntityUtility.GetSFProcessList();
        }
    }
}
