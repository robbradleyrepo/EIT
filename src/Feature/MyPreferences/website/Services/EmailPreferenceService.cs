namespace LionTrust.Feature.MyPreferences.Services
{
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Feature.MyPreferences.Repositories;
    using LionTrust.Foundation.Contact.Services;
    using Sitecore.Diagnostics;
    using System;

    public class EmailPreferencesService
    {
        private readonly IEmailPreferencesRepository _emailPreferencesRepository;
        private readonly IMailService _mailManager;

        public EmailPreferencesService(IEmailPreferencesRepository editEmailPreferencesRepository, IMailService mailManager)
        {
            _emailPreferencesRepository = editEmailPreferencesRepository;
            _mailManager = mailManager;
        }

        /// <summary>
        /// Get email preferences from Salesforce
        /// </summary>
        /// <param name="sfEntityId"></param>
        /// <param name="sfRandomGUID"></param>
        /// <param name="IsContact"></param>
        /// <returns></returns>
        public EmailPreferences GetEmailPreferences(string sfEntityId, string sfRandomGUID, bool isContact)
        {
            var emailPreferences = _emailPreferencesRepository.GetEmailPreferences(sfEntityId, sfRandomGUID, isContact);
            return emailPreferences;
        }

        /// <summary>
        /// Save email preferences to Salesforce
        /// </summary>
        /// <param name="emailPreferenceViewModel"></param>
        /// <returns></returns>
        public bool SaveEmailPreferences(EmailPreferences emailPreferences)
        {
            return _emailPreferencesRepository.SaveEmailPreferneces(emailPreferences);
        }

        /// <summary>
        /// Save non professional investor as a Salesforce Lead
        /// </summary>
        /// <param name="nonProfUserViewModel"></param>
        /// <returns></returns>
        public RegisterdUserWithEmailDetails SaveNonProfUserAsSFLead(NonProfessionalUser nonProfessionalUser, IEditEmailPrefTemplate emailTemplate, string preferencesUrl)
        {
            var returnedObj = _emailPreferencesRepository.SaveNonProfUserAsSFLead(nonProfessionalUser, emailTemplate, preferencesUrl);
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
        public RegisterdUserWithEmailDetails SaveProfUserAsSFContact(ProfessionalUser professionalUser, IEditEmailPrefTemplate emailTemplate, string preferencesUrl)
        {
            var registerdUserWithEmailDetails = _emailPreferencesRepository.SaveProfUserAsSFContact(professionalUser, emailTemplate, preferencesUrl);
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
        public bool ResendEditEmailPrefLink(string email, bool IsContact, IEditEmailPrefTemplate emailTemplate, string preferencesUrl)
        {
            try
            {
                var emailDetailObj = _emailPreferencesRepository.GetEmailDetailsForResendEmailPrefLink(email, IsContact, emailTemplate, preferencesUrl);
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
    }
}