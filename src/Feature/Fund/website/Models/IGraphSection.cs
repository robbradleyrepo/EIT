namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IGraphSection: IFundGlassBase
    {
        [SitecoreField(Constants.GraphSection.HeadingFieldId)]
        string Heading { get; set; }
    }
}