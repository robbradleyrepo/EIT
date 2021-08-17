using Glass.Mapper.Sc.Configuration.Attributes;
using LionTrust.Foundation.ORM.Models;

namespace LionTrust.Feature.MyPreferences.Models
{
    public interface IEditEmailPreferences : IGlassBase
    {
        [SitecoreField(Constants.EditEmailPreferences.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.Subtitle_FieldId)]
        string Subtitle { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.NewsTitle_FieldId)]
        string NewsTitle { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.NewsSubtitle_FieldId)]
        string NewsSubtitle { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.InstitutionalBulletinTitle_FieldId)]
        string InstitutionalBulletinTitle { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.InstitutionalBulletinSubtitle_FieldId)]
        string InstitutionalBulletinSubtitle { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.ProcessListTitle_FieldId)]
        string ProcessListTitle { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.ProcessListSubtitle_FieldId)]
        string ProcessListSubtitle { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.CheckboxInstructionText_FieldId)]
        string CheckboxInstructionText { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.SuccessPage_FieldId)]
        IGlassBase SuccessPage { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.FailedPage_FieldId)]
        IGlassBase FailedPage { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.PrivacyPolicyText_FieldId)]
        string PrivacyPolicyText { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.UnsubscribeAllText_FieldId)]
        string UnsubscribeAllText { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.GlobalSelectAllCheckboxText_FieldId)]
        string GlobalSelectAllCheckboxText { get; set; }

        [SitecoreField(Constants.EditEmailPreferences.GenericError_FieldId)]
        string GenericError { get; set; }
    }
}