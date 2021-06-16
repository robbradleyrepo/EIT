namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IGraphSection: IFundSelector
    {
        [SitecoreField(Constants.GraphSection.HeadingFieldId)]
        string Heading { get; set; }
    }
}