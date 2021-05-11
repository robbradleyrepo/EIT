namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System;

    public interface IFund : IFundOverviewData, ILegacyGlassBase
    {
        [SitecoreField(Constants.Fund.LaunchDate_FieldId)]
        DateTime LaunchDate { get; set; }

        [SitecoreField(Constants.Fund.FundSize_FieldId)]
        string FundSize { get; set; }

        [SitecoreField(Constants.Fund.NumberOfHoldings_FieldId)]
        string NumberOfHoldings { get; set; }

        [SitecoreField(Constants.Fund.MinimalInitialInvestment_FieldId)]
        string MinimalInitialInvestment { get; set; }

        [SitecoreField(Constants.Fund.MinimumAdditionalInvestment_FieldId)]
        string MinimumAdditionalInvestment { get; set; }
    }
}