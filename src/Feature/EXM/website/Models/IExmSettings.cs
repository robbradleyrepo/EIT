namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IExmSettings : IExmGlassBase
    {
        [SitecoreField(Constants.ExmSettings.TestingRecipientList_FieldID)]
        string TestingRecipientList { get; set; }

        [SitecoreField(Constants.ExmSettings.WhitelistDomains_FieldID)]
        string WhitelistDomains { get; set; }
    }
}