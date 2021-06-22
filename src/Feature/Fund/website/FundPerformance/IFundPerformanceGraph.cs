namespace LionTrust.Feature.Fund.FundPerformance
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Feature.Fund.Models;

    public interface IFundPerformanceGraph: IFundSelector
    {
        [SitecoreField(Constants.FundPerformanceGraph.HeadingFieldId)]
        string Heading { get; set; }

        [SitecoreField(Constants.FundPerformanceGraph.FactsheetTextFieldId)]
        string FactsheetText { get; set; }

        [SitecoreField(Constants.FundPerformanceGraph.DetailUrlFieldId)]
        Link DetailUrl { get; set; }
    }
}
