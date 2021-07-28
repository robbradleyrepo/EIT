namespace LionTrust.Foundation.Contact.Services
{
    using AutoMapper;
    using LionTrust.Foundation.Contact.Models.EditEmailPreferences;
    using LionTrust.Foundation.Contact.Repositories;
    using Sitecore.Diagnostics;
    using System;

    public class EmailPreferencesService
    {
        private readonly IEmailPreferencesRepository _emailPreferencesRepository;
        private readonly ILabelsRepository _labelsRepository;
        private readonly IMailService _mailManager;

        public EmailPreferencesService(IEmailPreferencesRepository editEmailPreferencesRepository, ILabelsRepository labelsRepository, IMailService mailManager)
        {
            _emailPreferencesRepository = editEmailPreferencesRepository;
            _labelsRepository = labelsRepository;
            _mailManager = mailManager;
        }

        /// <summary>
        /// Get email preferences from Salesforce
        /// </summary>
        /// <param name="sfEntityId"></param>
        /// <param name="sfRandomGUID"></param>
        /// <param name="IsContact"></param>
        /// <returns></returns>
        public EmailPreferencesViewModel GetEmailPreferences(string sfEntityId, string sfRandomGUID, bool isContact)
        {
            var model = new EmailPreferencesViewModel();
            var sfEmailPreferenceViewModel = _emailPreferencesRepository.GetEmailPreferences(sfEntityId, sfRandomGUID, isContact);
            if (sfEmailPreferenceViewModel != null)
            {
                model = Mapper.Map<EmailPreferencesViewModel>(sfEmailPreferenceViewModel);
                model.IsDataRetrievalSuccess = true;
            }
            else
            {
                model.IsDataRetrievalSuccess = false;
            }

            var currentPage = Sitecore.Context.Item;
            if (currentPage != null)
            {
                model.MainTitleText = currentPage["PageTitle"];
            }

            model = Mapper.Map(_labelsRepository.GetEmailPreferenceLabels(), model);
            return model;
        }

        /// <summary>
        /// Get page labels only 
        /// </summary>
        /// <returns></returns>
        public EmailPreferencesViewModel GetPreferenceCenterLabelsOnly()
        {
            var model = new EmailPreferencesViewModel();
            model = Mapper.Map(_labelsRepository.GetEmailPreferenceLabels(), model);

            var currentPage = Sitecore.Context.Item;
            if (currentPage != null)
            {
                model.MainTitleText = currentPage["PageTitle"];
            }

            return model;
        }

        /// <summary>
        /// Save email preferences to Salesforce
        /// </summary>
        /// <param name="emailPreferenceViewModel"></param>
        /// <returns></returns>
        public bool SaveEmailPreferences(EmailPreferencesViewModel emailPreferenceViewModel)
        {
            var tempEmailPreferenceObj = Mapper.Map<EmailPreferences>(emailPreferenceViewModel);
            return _emailPreferencesRepository.SaveEmailPreferneces(tempEmailPreferenceObj);
        }

        /// <summary>
        /// Save non professional investor as a Salesforce Lead
        /// </summary>
        /// <param name="nonProfUserViewModel"></param>
        /// <returns></returns>
        public ReturnedNonProfUserViewModel SaveNonProfUserAsSFLead(NonProfUserViewModel nonProfUserViewModel)
        {
            var nonProfUserObj = Mapper.Map<NonProfessionalUser>(nonProfUserViewModel);
            var returnedObj = _emailPreferencesRepository.SaveNonProfUserAsSFLead(nonProfUserObj);
            if (returnedObj != null)
            {
                //Send email to the the new users with the link to edit email preferences
                if (!returnedObj.IsUserExists)
                {
                    _mailManager.SendEmail(returnedObj.FromAddress, returnedObj.FromDisplyName, returnedObj.ToAddresses, returnedObj.Subject,
                                                       returnedObj.Message, true);
                    Log.Info(string.Format("Email sent with the edit email preference link to - {0}", returnedObj.ToAddresses), this);
                }
                return new ReturnedNonProfUserViewModel { IsUserExists = returnedObj.IsUserExists };
            }

            return null;
        }

        /// <summary>
        /// Save professional investors as a Salesforce Contact
        /// </summary>
        /// <param name="nonProfUserViewModel"></param>
        /// <returns></returns>
        public ReturnedProfUserViewModel SaveProfUserAsSFContact(ProfUserViewModel nonProfUserViewModel)
        {
            var nonProfUserObj = Mapper.Map<ProfessionalUser>(nonProfUserViewModel);
            var returnedObj = _emailPreferencesRepository.SaveProfUserAsSFContact(nonProfUserObj);
            if (returnedObj != null)
            {
                //Send email to the the new users with the link to edit email preferences
                if (!returnedObj.IsUserExists)
                {
                    _mailManager.SendEmail(returnedObj.FromAddress, returnedObj.FromDisplyName, returnedObj.ToAddresses, returnedObj.Subject,
                                                       returnedObj.Message, true);
                    Log.Info(string.Format("Email sent with the edit email preference link to - {0}", returnedObj.ToAddresses), this);
                }
                return new ReturnedProfUserViewModel { IsUserExists = returnedObj.IsUserExists };
            }

            return null;
        }

        /// <summary>
        /// Get labels for non professional user registration page
        /// </summary>
        /// <returns></returns>
        public RegisterNonProfUserViewModel GetRegisterNonProfUserUIDetails()
        {
            var model = new RegisterNonProfUserViewModel();
            model = Mapper.Map(_labelsRepository.GetRegisterNonProfUserLabels(), model);
            return model;
        }

        /// <summary>
        /// Get labels for professional investor registration page
        /// </summary>
        /// <returns></returns>
        public RegisterProfUserViewModel GetRegisterProfUserUIDetails()
        {
            var model = new RegisterProfUserViewModel();
            model = Mapper.Map(_labelsRepository.GetRegisterProfUserLabels(), model);
            return model;
        }

        /// <summary>
        /// Resend edit email pref link
        /// </summary>
        /// <returns></returns>
        public bool ResendEditEmailPrefLink(string email, bool IsContact)
        {
            try
            {
                var emailDetailObj = _emailPreferencesRepository.GetEmailDetailsForResendEmailPrefLink(email, IsContact);
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

        public ResendEmailPrefLinkViewModel GetUIDetailsForResendEmailPrefEmail(bool isSuccess)
        {
            var model = new ResendEmailPrefLinkViewModel();
            model = Mapper.Map(_emailPreferencesRepository.GetUIDetailsForResendEmailPrefEmail(isSuccess), model);
            return model;
        }

        /// <summary>
        /// Get redirect URL after form submits
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <returns></returns>
        public string GetRedirectUrlAfterEditing(bool isSuccess)
        {
            return _emailPreferencesRepository.GetRedirectUrlAfterEditing(isSuccess);
        }
    }
}