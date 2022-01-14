namespace LionTrust.Feature.EXM.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Navigation.Models;

    public interface ISmallPrint : IExmGlassBase
    {
        [SitecoreField(Constants.SmallPrint.ExtraLinks_FieldID)]
        IEnumerable<IPageLink> ExtraLinks { get; set; }

        [SitecoreField(Constants.SmallPrint.SmallBodyText_FieldID)]
        string SmallBodyText { get; set; }

        [SitecoreField(Constants.SmallPrint.EmailPreferencesText_FieldID)]
        string EmailPreferencesText { get; set; }
    }
}