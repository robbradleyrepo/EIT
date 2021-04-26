namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFundPage : IHeroHomePageData
    {
        [SitecoreField(Constants.Fund.FundReference_FieldId, SitecoreFieldType.Droplink, "Fund page data")]
        IFund FundReference { get; set; }
    }
}