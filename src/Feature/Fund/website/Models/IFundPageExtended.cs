namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;

    public interface IFundPageExtended : IFundPage
    {
        [SitecoreField(Foundation.Legacy.Constants.FundPage.FundReference_FieldId)]
        IFundExtended Fund { get; set; }
    }
}