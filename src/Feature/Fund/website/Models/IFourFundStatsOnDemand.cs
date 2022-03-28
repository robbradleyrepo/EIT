namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFourFundStatsOnDemand : IFundGlassBase
    {       
        [SitecoreField(Constants.FourFundStatsOnDemand.SharePriceLabel_FieldId, SitecoreFieldType.SingleLineText, "Four Fund Stats On Demand")]
        string SharePriceLabel { get; set; }

        [SitecoreField(Constants.FourFundStatsOnDemand.CompanyLaunchDateLabel_FieldId, SitecoreFieldType.SingleLineText, "Four Fund Stats On Demand")]
        string CompanyLaunchDateLabel { get; set; }

        [SitecoreField(Constants.FourFundStatsOnDemand.CompanyLaunchDateValue_FieldId, SitecoreFieldType.SingleLineText, "Four Fund Stats On Demand")]
        string CompanyLaunchDateValue { get; set; }

        [SitecoreField(Constants.FourFundStatsOnDemand.ManagerInceptionDateLabel_FieldId, SitecoreFieldType.SingleLineText, "Four Fund Stats On Demand")]
        string ManagerInceptionDateLabel { get; set; }

        [SitecoreField(Constants.FourFundStatsOnDemand.ManagerInceptionDateValue_FieldId, SitecoreFieldType.SingleLineText, "Four Fund Stats On Demand")]
        string ManagerInceptionDateValue { get; set; }

        [SitecoreField(Constants.FourFundStatsOnDemand.TotalAssetsLabel_FieldId, SitecoreFieldType.SingleLineText, "Four Fund Stats On Demand")]
        string TotalAssetsLabel { get; set; }

        [SitecoreField(Constants.FourFundStatsOnDemand.TotalAssetsValue_FieldId, SitecoreFieldType.SingleLineText, "Four Fund Stats On Demand")]
        string TotalAssetsValue { get; set; }
    }
}