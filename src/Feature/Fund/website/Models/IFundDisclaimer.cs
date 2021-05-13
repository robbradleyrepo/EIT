namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFundDisclaimer : IFundGlassBase
    {
        [SitecoreField(Constants.FundDisclaimer.SmallSize_FieldId)]
        bool SmallSize { get; set; }

        [SitecoreField(Constants.FundDisclaimer.TextColor_FieldId)]
        Foundation.Design.ILookupValue TextColor { get; set; }
    }
}