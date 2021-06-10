namespace LionTrust.Foundation.SitecoreForms.Factories
{
    using System;

    using LionTrust.Foundation.SitecoreForms.Models;
    
    public interface ISitecoreFormsCustomSaveActtionRepository
    {
        SendEmailTemplate GetTemplateForSendEmailSaveAction(Guid itemId);
    }
}
