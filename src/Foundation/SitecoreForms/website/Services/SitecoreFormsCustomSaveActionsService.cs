namespace LionTrust.Foundation.SitecoreForms.Services
{
    using System;
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.SitecoreForms.Factories;
    using LionTrust.Foundation.SitecoreForms.Models;

    /// <summary>
    /// Custom save action services for Sitecore forms
    /// </summary>
    [Service]
    public class SitecoreFormsCustomSaveActionsService : ISitecoreFormsCustomSaveActionsService
    {
        private readonly ISitecoreFormsCustomSaveActionRepository _customSaveActionRepository;
        private readonly IMailManager _mailManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="viewModelFactory"></param>
        /// <param name="customSaveActionRepository"></param>
        /// <param name="mailManager"></param>
        public SitecoreFormsCustomSaveActionsService(ISitecoreFormsCustomSaveActionRepository customSaveActionRepository, IMailManager mailManager)
        {
            _customSaveActionRepository = customSaveActionRepository;
            _mailManager = mailManager;
        }

        /// <summary>
        /// Get template for the email which send by the custom save action
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        public ISaveActionSendEmailTemplate GetEmailTemplate(Guid referenceId)
        {
            return _customSaveActionRepository.GetTemplateForSendEmailSaveAction(referenceId);
        }

        //Send email
        public void SendEmailAsCustomSaveAction(string fromAddress, string fromName, string toAddresses, string ccAddress, string bccAddress, string subject, string message, bool isHtml)
        {
            _mailManager.SendEmail(fromAddress, fromName, toAddresses, ccAddress, bccAddress, subject, message, true);
        }
    }
}
