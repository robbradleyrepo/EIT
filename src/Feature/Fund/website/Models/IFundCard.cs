namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;

    public interface IFundCard: IFundGlassBase
    {
        [SitecoreField(Constants.FundCard.DescriptionFieldId)]
        string Description { get; set; }

        [SitecoreField(Constants.FundCard.FundFieldId)]
        IFundPage FundPage { get; set; }
    }
}
