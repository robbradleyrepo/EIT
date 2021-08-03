namespace LionTrust.Feature.MyPreferences.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IEditEmailPrefTemplate : IGlassBase
    {
        [SitecoreField(Constants.EditEmailPrefTemplate.FromAddress_FieldId)]
        string FromAddress { get; set; }

        [SitecoreField(Constants.EditEmailPrefTemplate.FromDisplayName_FieldId)]
        string FromDisplayName { get; set; }

        [SitecoreField(Constants.EditEmailPrefTemplate.Subject_FieldId)]
        string Subject { get; set; }

        [SitecoreField(Constants.EditEmailPrefTemplate.Message_FieldId)]
        string Message { get; set; }
    }
}
