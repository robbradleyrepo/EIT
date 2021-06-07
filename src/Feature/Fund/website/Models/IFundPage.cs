namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IFundPage: Foundation.Legacy.Models.IFund
    {
        [SitecoreField(Constants.FundPage.PageFieldId)]
        Link Page { get; set; }
    }
}
