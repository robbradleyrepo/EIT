namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IFundDisclaimer : IFundGlassBase
    {
        [SitecoreField(Constants.FundDisclaimer.SmallSize_FieldId)]
        bool SmallSize { get; set; }

        [SitecoreField(Constants.FundDisclaimer.TextColor_FieldId)]
        Foundation.Design.ILookupValue TextColor { get; set; }

        [SitecoreField(Constants.FundDisclaimer.BackgroundImage_FieldId)]
        Image BackgroundImage { get; set; }

        [SitecoreField(Constants.FundDisclaimer.UseBackgroundImage_FieldId)]
        bool UseBackgroundImage { get; set; }
    }
}