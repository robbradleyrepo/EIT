namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IDisclaimer : IExmGlassBase
    {
        [SitecoreField(Constants.Disclaimer.SmallBodyText_FieldID)]
        string SmallBodyText { get; set; }

        [SitecoreField(Constants.Disclaimer.EmailPreferencesText_FieldID)]
        string EmailPreferencesText { get; set; }
    }
}