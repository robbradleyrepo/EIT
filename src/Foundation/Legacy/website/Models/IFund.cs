namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System;
    using System.Collections.Generic;

    public interface IFund : IFundOverviewData, ILegacyGlassBase
    {
        [SitecoreField(Constants.Fund.FundManagersFieldId, SectionName ="Fund Info")]
        IEnumerable<IAuthor> FundManagers { get; set; }

        [SitecoreField(Constants.Fund.LaunchDate_FieldId, SitecoreFieldType.Date,  "Fund Info")]
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