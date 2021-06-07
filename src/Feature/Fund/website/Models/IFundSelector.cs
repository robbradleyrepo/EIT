namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFundSelector: IFundGlassBase
    {
        [SitecoreField(Constants.FundSelector.FundFieldId)]
        Foundation.Legacy.Models.IFund Fund { get; set; }
    }
}
