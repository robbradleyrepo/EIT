namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System;

    public interface IFund : IFundOverviewData, ILegacyGlassBase
    {
        [SitecoreField(Constants.Fund.LaunchDate_FieldId, SitecoreFieldType.Date, "Fund Info")]
        DateTime LaunchDate { get; set; }

        [SitecoreField(Constants.Fund.FundSize_FieldId, SitecoreFieldType.SingleLineText, "Fund Info")]
        string FundSize { get; set; }

        [SitecoreField(Constants.Fund.NumberOfHoldings_FieldId, SitecoreFieldType.SingleLineText, "Fund Info")]
        string NumberOfHoldings { get; set; }
    }
}