namespace LionTrust.Foundation.SitecoreForms.Factories
{
    using System;

    using LionTrust.Foundation.SitecoreForms.Models;
    
    public interface ISitecoreFormsCustomSaveActionRepository
    {
        ISaveActionSendEmailTemplate GetTemplateForSendEmailSaveAction(Guid itemId);
    }
}
