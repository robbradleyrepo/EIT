namespace LionTrust.Feature.EXM.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Navigation.Models;

    public interface IDisclaimer : IExmGlassBase
    {
        [SitecoreField(Constants.Disclaimer.SmallBodyText_FieldID)]
        string SmallBodyText { get; set; }

        [SitecoreField(Constants.Disclaimer.EmailPreferencesText_FieldID)]
        string EmailPreferencesText { get; set; }

        [SitecoreField(Constants.Disclaimer.EmailPreferencesTextBrowser_FieldID)]
        string EmailPreferencesTextBrowser { get; set; }
    }
}