namespace LionTrust.Feature.Banner.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IRiskBanner : IBannerGlassBase
    {
        [SitecoreField(Constants.RiskBanner.BannerTextFieldId)]
        string RiskBannerText { get; set; }
    }
}