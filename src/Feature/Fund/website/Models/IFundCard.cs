namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.Legacy.Models;

    public interface IFundCard: IFundGlassBase
    {
        [SitecoreField(Constants.FundCard.ImageFieldId)]
        Image Image{ get; set; }

        [SitecoreField(Constants.FundCard.HeadingFieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.FundCard.DescriptionFieldId)]
        string Description { get; set; }

        [SitecoreField(Constants.FundCard.FundFieldId)]
        IFundPage FundPage { get; set; }
    }
}
