namespace LionTrust.Feature.Fund
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Fund.Models;

    public interface ISectorBreakdown: IFundSelector
    {
        [SitecoreField(Constants.SectorBreakdown.HeadingFieldId)]
        string Heading { get; set; }
    }
}
