namespace LionTrust.Feature.MyPreferences.Services
{
    using LionTrust.Feature.MyPreferences.Helpers;
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Feature.MyPreferences.Repositories;
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Foundation.Contact.Models;
    using LionTrust.Foundation.Contact.Services;
    using Sitecore.Diagnostics;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EmailPreferencesService : IEmailPreferencesService
    {
        private readonly IEmailPreferencesRepository _emailPreferencesRepository;
        private readonly IMailManager _mailManager;
        private readonly IPersonalizedContentService _personalizedContentService;
        private readonly IEmailHelper _emailHelper;
        private readonly ISFEntityUtility _sfEntityUtility;

        public EmailPreferencesService(
            IEmailPreferencesRepository editEmailPreferencesRepository, 
            IMailManager mailManager, 
            IPersonalizedContentService personalizedContentService,
            IEmailHelper emailHelper,
            ISFEntityUtility sfEntityUtility)
        {
            _emailPreferencesRepository = editEmailPreferencesRepository;
            _mailManager = mailManager;
            _personalizedContentService = personalizedContentService;
            _emailHelper = emailHelper;
            _sfEntityUtility = sfEntityUtility;
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
                else
                {
                    Log.Info(string.Format("Email is not recognized - {0}", email), this);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this);
            }

            return false;
        }

        public void SendAutomatedWelcomeEmails(IRegisterInvestor registerInvestor, IAutomatedWelcomeSettings settings)
        {
            try
            {
                var entityErrors = new List<string>();

                var date = DateTime.Now.AddDays(-1).ToUniversalTime();
                var entityList = _sfEntityUtility.GetEntitiesToSendWecomeEmail(date);

                var preferencesUrl = registerInvestor.EditPreferencesPage.AbsoluteUrl;
                var fundDashboardUrl = registerInvestor.FundDashboardPage.AbsoluteUrl;

                Log.Info(string.Format("[AutomatedWelcomeEmail] {0} emails to be sent", entityList.Count), this);

                var count = 0;
                foreach (var entity in entityList)
                {
                    try
                    {
                        var ukResident = entity.InternalFields.GetField<bool>(Foundation.Contact.Constants.SF_UKResident);
                        var emailTemplate = ukResident ? registerInvestor.UKEmailTemplate
                                                        : registerInvestor.NonUKEmailTemplate;

                        var fromAddress = emailTemplate.FromAddress;
                        var fromDisplayName = emailTemplate.FromDisplayName;
                        var toAddresses = entity.InternalFields[Foundation.Contact.Constants.SF_EmailField];
                        var subject = emailTemplate.Subject;

                        var fullName = _sfEntityUtility.GetFullName(entity.InternalFields);
                        var randomGuid = entity.InternalFields[Foundation.Contact.Constants.SF_GUIDForEmailPref];
                        var editEmailPrefPagelink = _sfEntityUtility.GetEditEmailPrefPageLink(preferencesUrl, randomGuid, entity.Id);
                        var fundDashboardlink = _sfEntityUtility.GetFundDashboardLink(fundDashboardUrl, randomGuid, entity.Id);

                        var message = _emailHelper.GenerateEmailMessageBody(emailTemplate.Message, fullName, editEmailPrefPagelink, fundDashboardlink);

                        _mailManager.SendEmail(fromAddress, fromDisplayName, toAddresses, subject, message, true);
                        count++;
                    }
                    catch (Exception ex)
                    {
                        entityErrors.Add(entity.Id);
                        Log.Error(string.Format("[AutomatedWelcomeEmail] Error sending welcome email. Entity Id: {0}", entity.Id), ex, this);
                    }
                }

                Log.Info(string.Format("[AutomatedWelcomeEmail] {0} emails have been sent", count), this);

                if (entityErrors.Any())
                {
                    var message = string.Format("Error sending welcome email to these Salesforce entities: {0}", string.Join(", ", entityErrors));
                    SendEmailOnError(message, null, settings);
                }
            }
            catch (Exception ex)
            {
                var message = string.Format("Error sending welcome email: {0}", ex.Message);
                SendEmailOnError(message, ex, settings);
            }
        }

        public IEnumerable<SFProcess> GetSFProcessList()
        {
            return _emailPreferencesRepository.GetSFProcessList();
        }

        private void SendEmailOnError(string message, Exception exception, IAutomatedWelcomeSettings settings)
        {
            if (exception != null)
            {
                Log.Error(string.Format("[AutomatedWelcomeEmail] {0}", message), exception, this);
            }
            else
            {
                Log.Error(string.Format("[AutomatedWelcomeEmail] {0}", message), this);
            }

            try
            {
                if (settings == null)
                {
                    return;
                }

                _mailManager.SendEmail(settings.FromAddress, settings.FromDisplayName, settings.ToAddresses, settings.Subject, message, true);
            }
            catch (Exception ex)
            {
                Log.Error("[AutomatedWelcomeEmail]: Error sending email with automated welcome email information", ex, this);
            }
        }
    }
}