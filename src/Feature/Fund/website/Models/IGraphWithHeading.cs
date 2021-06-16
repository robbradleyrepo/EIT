namespace LionTrust.Feature.Fund.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IGraphWithHeading: IFundSelector
    {
        [SitecoreField(Constants.GraphWithHeading.HeadingFieldId)]
        string Heading { get; set; }
    }
}
