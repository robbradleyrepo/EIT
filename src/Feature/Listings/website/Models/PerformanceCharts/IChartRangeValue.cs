namespace LionTrust.Feature.Listings.Models.PerformanceCharts
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    [SitecoreType(TemplateId = Constants.ChartRangeValues.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface IChartRangeValue : IListingsGlassBase
    {
        [SitecoreField(Constants.ChartRangeValues.Value_FieldId)]
        string Value { get; set; }
    }
}