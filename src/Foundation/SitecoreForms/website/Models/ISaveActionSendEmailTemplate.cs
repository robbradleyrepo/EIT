namespace LionTrust.Foundation.SitecoreForms.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface ISaveActionSendEmailTemplate : IGlassBase
    {
        [SitecoreField(Constants.SendEmailSaveActionEmailTemplate.Subject_FieldId)]
        string Subject { get; set; }

        [SitecoreField(Constants.SendEmailSaveActionEmailTemplate.From_FieldId)]
        string From { get; set; }

        [SitecoreField(Constants.SendEmailSaveActionEmailTemplate.FromDisplayName_FieldId)]
        string FromDisplayName { get; set; }

        [SitecoreField(Constants.SendEmailSaveActionEmailTemplate.To_FieldId)]
        string To { get; set; }

        [SitecoreField(Constants.SendEmailSaveActionEmailTemplate.CC_FieldId)]
        string CC { get; set; }

        [SitecoreField(Constants.SendEmailSaveActionEmailTemplate.BCC_FieldId)]
        string BCC { get; set; }

        [SitecoreField(Constants.SendEmailSaveActionEmailTemplate.Message_FieldId)]
        string Message { get; set; }
    }
}