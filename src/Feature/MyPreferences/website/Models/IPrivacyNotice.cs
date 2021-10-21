namespace LionTrust.Feature.MyPreferences.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IPrivacyNotice : IGlassBase
    {
        [SitecoreField(Constants.PrivacyNotice.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.PrivacyNotice.Text_FieldId)]
        string Text { get; set; }
    }
}