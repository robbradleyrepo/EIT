namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFundOverviewData : ILegacyGlassBase
    {
        [SitecoreField(Constants.FundOverviewData.OverviewTitle_FieldId, SitecoreFieldType.SingleLineText, "Overview")]
        string OverviewTitle { get; set; }

        [SitecoreField(Constants.FundOverviewData.OverviewText_FieldId, SitecoreFieldType.RichText, "Overview")]
        string OverviewText { get; set; }
    }
}