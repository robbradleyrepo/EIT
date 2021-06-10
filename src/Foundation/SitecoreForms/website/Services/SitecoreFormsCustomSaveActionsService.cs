namespace LionTrust.Foundation.SitecoreForms.Services
{
    using System;
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.SitecoreForms.Factories;
    using LionTrust.Foundation.SitecoreForms.Managers;
    using LionTrust.Foundation.SitecoreForms.Models;

    /// <summary>
    /// Custom save action services for Sitecore forms
    /// </summary>
    [Service]
    public class SitecoreFormsCustomSaveActionsService : ISitecoreFormsCustomSaveActionsService
    {
        private readonly ISitecoreFormsCustomSaveActtionRepository _customSaveActionRepository;
        private readonly IMailManager _mailManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="viewModelFactory"></param>
        /// <param name="customSaveActionRepository"></param>
        /// <param name="mailManager"></param>
        public SitecoreFormsCustomSaveActionsService(ISitecoreFormsCustomSaveActtionRepository customSaveActionRepository, IMailManager mailManager)
        {
            _customSaveActionRepository = customSaveActionRepository;
            _mailManager = mailManager;
        }

        /// <summary>
        /// Get template for the email which send by the custom save action
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        public SendEmailTemplateViewModel GetEmailTemplate(Guid referenceId)
        {
            var emailTemplate = _customSaveActionRepository.GetTemplateForSendEmailSaveAction(referenceId);
            return new SendEmailTemplateViewModel()
            {
                BCC = emailTemplate.BCC,
                CC = emailTemplate.CC,
                From = emailTemplate.From,
                FromDisplyName = emailTemplate.FromDisplyName,
                Message = emailTemplate.Message,
                Subject = emailTemplate.Subject,
                To = emailTemplate.To
            };
        }

        //Send email
        public void SendEmailAsCustomSaveAction(string fromAddress, string fromName, string toAddresses, string ccAddress, string bccAddress, string subject, string message, bool isHtml)
        {
            _mailManager.SendEmail(fromAddress, fromName, toAddresses, ccAddress, bccAddress, subject, message, true);
        }
    }
}
