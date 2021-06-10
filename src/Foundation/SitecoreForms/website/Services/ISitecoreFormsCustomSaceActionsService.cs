namespace LionTrust.Foundation.SitecoreForms.Services
{
    using System;

    using LionTrust.Foundation.SitecoreForms.Models;
    
    public interface ISitecoreFormsCustomSaveActionsService
    {
        ISaveActionSendEmailTemplate GetEmailTemplate(Guid referenceId);
        void SendEmailAsCustomSaveAction(string fromAddress, string fromName, string toAddresses, string ccAddress, string bccAddress, string subject, string message, bool isHtml);
    }
}