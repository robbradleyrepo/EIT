namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IKeyInfoPriceComponent : IFundGlassBase
    {
        [SitecoreField(Constants.KeyInfoPriceComponent.)]
        IFund Fund { get; set; }
    }
}