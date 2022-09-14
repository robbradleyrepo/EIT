namespace LionTrust.Feature.MyPreferences.Repositories
{
    using LionTrust.Foundation.Contact.Models;
    using System;
    using LionTrust.Foundation.Contact.Services;
    using System.Collections.Generic;
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Feature.MyPreferences.Helpers;

    public class EmailPreferencesRepository : IEmailPreferencesRepository
    {
        private readonly IApplicationCacheRepository _applicationCacheRepository;
        private readonly IMailManager _mailManager;
        private readonly IEmailHelper _emailHelper;

        private const string SFContactCountryListCacheKey = "salesforce-contact-country-list";
        private const string SFLeadCountryListCacheKey = "salesforce-lead-country-list";

        public EmailPreferencesRepository(IApplicationCacheRepository applicationCacheRepository, IMailManager mailManager, IEmailHelper emailHelper)
        {
            _applicationCacheRepository = applicationCacheRepository;
            _mailManager = mailManager;
            _emailHelper = emailHelper;
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
                var emailMessageBody = _emailHelper.GenerateEmailMessageBody(emailTemplate.Message, savedUserEmailDetails.FullName, savedUserEmailDetails.EditEmailPrefLink, savedUserEmailDetails.FundDashboardLink);

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
                var emailMessageBody = _emailHelper.GenerateEmailMessageBody(emailTemplate.Message, savedUserEmailDetails.FullName, savedUserEmailDetails.EditEmailPrefLink, savedUserEmailDetails.FundDashboardLink);

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
                var emailMessageBody = _emailHelper.GenerateEmailMessageBody(emailTemplate.Message, userDetails.FullName, userDetails.EditEmailPrefLink, userDetails.FundDashboardLink);

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
