namespace LionTrust.Foundation.SitecoreForms.Factories
{
    using LionTrust.Foundation.SitecoreForms.Models;
    using System;

    public class SitecoreFormsCustomSaveActtionRepository : BaseRepository, ISitecoreFormsCustomSaveActtionRepository
    {
        public SitecoreFormsCustomSaveActtionRepository(IEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public SendEmailTemplate GetTemplateForSendEmailSaveAction(Guid itemId)
        {
            var emailTemplateItem = GetItem(itemId);
            return EntityFactory.Build<SendEmailTemplate>(emailTemplateItem);
        }
    }
}
