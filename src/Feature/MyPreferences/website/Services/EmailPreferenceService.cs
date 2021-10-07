namespace LionTrust.Feature.MyPreferences.Services
{
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Feature.MyPreferences.Repositories;
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Foundation.Contact.Models;
    using LionTrust.Foundation.Contact.Services;
    using Sitecore.Diagnostics;
    using System;
    using System.Collections.Generic;

    public class EmailPreferencesService
    {
        private readonly IEmailPreferencesRepository _emailPreferencesRepository;
        private readonly IMailManager _mailManager;
        private readonly IPersonalizedContentService _personalizedContentService;

        public EmailPreferencesService(IEmailPreferencesRepository editEmailPreferencesRepository, IMailManager mailManager, IPersonalizedContentService personalizedContentService)
        {
            _emailPreferencesRepository = editEmailPreferencesRepository;
            _mailManager = mailManager;
            _personalizedContentService = personalizedContentService;
        }

        /// <summary>
        /// Save email preferences to Salesforce
        /// </summary>
        /// <param name="emailPreferenceViewModel"></param>
        /// <returns></returns>
        public bool SaveEmailPreferences(Context context)
        {
            var result = _emailPreferencesRepository.SaveEmailPreferneces(context);

            if (result)
            {
                _personalizedContentService.UpdateContext(context);
            }

            return result;
        }

        /// <summary>
        /// Save non professional investor as a Salesforce Lead
        /// </summary>
        /// <param name="nonProfUserViewModel"></param>
        /// <returns></returns>
        public RegisterdUserWithEmailDetails SaveNonProfUserAsSFLead(NonProfessionalUser nonProfessionalUser, IEditEmailPrefTemplate emailTemplate, string preferencesUrl, string fundDashboardUrl)
        {
            var returnedObj = _emailPreferencesRepository.SaveNonProfUserAsSFLead(nonProfessionalUser, emailTemplate, preferencesUrl, fundDashboardUrl);
            if (returnedObj != null)
            {
                //Send email to the the new users with the link to edit email preferences
                if (!returnedObj.IsUserExists)
                {
                    _mailManager.SendEmail(returnedObj.FromAddress, returnedObj.FromDisplyName, returnedObj.ToAddresses, returnedObj.Subject,
                                                       returnedObj.Message, true);
                    Log.Info(string.Format("Email sent with the edit email preference link to - {0}", returnedObj.ToAddresses), this);
                }
                return returnedObj;
            }

            return null;
        }

        /// <summary>
        /// Save professional investors as a Salesforce Contact
        /// </summary>
        /// <param name="nonProfUserViewModel"></param>
        /// <returns></returns>
        public RegisterdUserWithEmailDetails SaveProfUserAsSFContact(ProfessionalUser professionalUser, IEditEmailPrefTemplate emailTemplate, string preferencesUrl, string fundDashboardUrl)
        {
            var registerdUserWithEmailDetails = _emailPreferencesRepository.SaveProfUserAsSFContact(professionalUser, emailTemplate, preferencesUrl, fundDashboardUrl);
            if (registerdUserWithEmailDetails != null)
            {
                //Send email to the the new users with the link to edit email preferences
                if (!registerdUserWithEmailDetails.IsUserExists)
                {
                    _mailManager.SendEmail(registerdUserWithEmailDetails.FromAddress, registerdUserWithEmailDetails.FromDisplyName, registerdUserWithEmailDetails.ToAddresses, registerdUserWithEmailDetails.Subject,
                                                       registerdUserWithEmailDetails.Message, true);
                    Log.Info(string.Format("Email sent with the edit email preference link to - {0}", registerdUserWithEmailDetails.ToAddresses), this);
                }
                return registerdUserWithEmailDetails;
            }

            return null;
        }

        /// <summary>
        /// Resend edit email pref link
        /// </summary>
        /// <returns></returns>
        public bool ResendEditEmailPrefLink(string email, bool IsContact, IEditEmailPrefTemplate emailTemplate, string preferencesUrl, string fundDashboardUrl)
        {
            try
            {
                var emailDetailObj = _emailPreferencesRepository.GetEmailDetailsForResendEmailPrefLink(email, IsContact, emailTemplate, preferencesUrl, fundDashboardUrl);
                if (emailDetailObj != null)
                {
                    _mailManager.SendEmail(emailDetailObj.FromAddress, emailDetailObj.FromDisplyName, emailDetailObj.ToAddresses, emailDetailObj.Subject,
                                                           emailDetailObj.Message, true);
                    Log.Info(string.Format("Resent edit email preference link email to - {0}", email), this);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
            }

            return false;
        }

        public IEnumerable<SFProcess> GetSFProcessList()
        {
            return _emailPreferencesRepository.GetSFProcessList();
        }
    }
}