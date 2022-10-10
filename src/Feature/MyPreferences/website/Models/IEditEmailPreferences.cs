using Glass.Mapper.Sc.Configuration.Attributes;
using LionTrust.Foundation.ORM.Models;
using System;

namespace LionTrust.Feature.MyPreferences.Models
{
    public interface IEditEmailPreferences : IGlassBase
    {
        [SitecoreField(Constants.EditPreferences.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.EditPreferences.InstitutionalBulletinLabel_FieldId)]
        string InstitutionalBulletinTitle { get; set; }

        [SitecoreField(Constants.EditPreferences.InstitutionalBulletinText_FieldId)]
        string InstitutionalBulletinSubtitle { get; set; }

        [SitecoreField(Constants.EditPreferences.SuccessPage_FieldId)]
        IGlassBase SuccessPage { get; set; }

        [SitecoreField(Constants.EditPreferences.UnsubscribeTortoiseNewsletterLabel_FieldId)]
        string UnsubscribeTortoiseNewsletterLabel { get; set; }

        [SitecoreField(Constants.EditPreferences.UnsubscribeTortoiseNewsletterText_FieldId)]
        string UnsubscribeTortoiseNewsletterText { get; set; }

        [SitecoreField(Constants.EditPreferences.UnsubscribeAllLabel_FieldId)]
        string UnsubscribeAllLabel { get; set; }

        [SitecoreField(Constants.EditPreferences.UnsubscribeAllText_FieldId)]
        string UnsubscribeAllText { get; set; }

        [SitecoreField(Constants.EditPreferences.GenericError_FieldId)]
        string GenericError { get; set; }

        [SitecoreField(Constants.EditPreferences.SubmitCTAText_FieldId)]
        string SubmitCTAText { get; set; }

        [SitecoreField(Constants.EditPreferences.SubscribeGoal_FieldId)]
        Guid SubscribeGoal { get; set; }

        [SitecoreField(Constants.EditPreferences.UnsubscribeGoal_FieldId)]
        Guid UnsubscribeGoal { get; set; }
    }
}