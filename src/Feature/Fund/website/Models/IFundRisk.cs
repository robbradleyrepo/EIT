using Glass.Mapper.Sc.Configuration.Attributes;

namespace LionTrust.Feature.Fund.Models
{
    public interface IFundRisk : IFundGlassBase
    {
        [SitecoreField(Constants.FundRisk.BannerText_FieldId)]
        string RiskBannerText { get; set; }
    }
}