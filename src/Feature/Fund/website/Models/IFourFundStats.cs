namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;

    public interface IFourFundStats : IFundGlassBase
    {
        [SitecoreField(Constants.FourFundStats.FundFieldId)]
        IFund Fund { get; set; } 

        [SitecoreField(Constants.FourFundStats.FundLaunchDateLabel_FieldId, SitecoreFieldType.SingleLineText, "Four Fund Stats")]
        string FundLaunchDateLabel { get; set; }

        [SitecoreField(Constants.FourFundStats.FundSizeLabel_FieldId, SitecoreFieldType.SingleLineText, "Four Fund Stats")]
        string FundSizeLabel { get; set; }

        [SitecoreField(Constants.FourFundStats.NumberOfHoldingsLabel_FieldId, SitecoreFieldType.SingleLineText, "Four Fund Stats")]
        string NumberOfHoldingsLabel { get; set; }

        [SitecoreField(Constants.FourFundStats.ManagedByCurrentTeamForLabel_FieldId, SitecoreFieldType.SingleLineText, "Four Fund Stats")]
        string ManagedByCurrentTeamForLabel { get; set; }

        [SitecoreField(Constants.FourFundStats.ManagedByCurrentTeamForValue_FieldId, SitecoreFieldType.SingleLineText, "Four Fund Stats")]
        string ManagedByCurrentTeamForValue { get; set; }
    }
}