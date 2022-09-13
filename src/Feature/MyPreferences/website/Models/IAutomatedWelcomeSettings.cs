namespace LionTrust.Feature.MyPreferences.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IAutomatedWelcomeSettings : IGlassBase
    {
        [SitecoreField(Constants.AutomatedWelcomeSettings.Enabled_FieldID)]
        bool Enabled { get; set; }

        [SitecoreField(Constants.AutomatedWelcomeSettings.FromAddress_FieldID)]
        string FromAddress { get; set; }

        [SitecoreField(Constants.AutomatedWelcomeSettings.FromDisplayName_FieldID)]
        string FromDisplayName { get; set; }

        [SitecoreField(Constants.AutomatedWelcomeSettings.ToAddress_FieldID)]
        string ToAddresses { get; set; }

        [SitecoreField(Constants.AutomatedWelcomeSettings.Subject_FieldID)]
        string Subject { get; set; }
    }
}