namespace LionTrust.Foundation.SitecoreForms.Factories
{
    using System;

    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Foundation.SitecoreForms.Models;
   
    public class SitecoreFormsCustomSaveActionRepository : ISitecoreFormsCustomSaveActionRepository
    {
        private readonly IMvcContext _mvcContext;

        public SitecoreFormsCustomSaveActionRepository(IMvcContext mvcContext)
        {
            _mvcContext = mvcContext;
        }

        public ISaveActionSendEmailTemplate GetTemplateForSendEmailSaveAction(Guid itemId)
        {
            return _mvcContext.SitecoreService.GetItem<ISaveActionSendEmailTemplate>(itemId);           
        }
    }
}
