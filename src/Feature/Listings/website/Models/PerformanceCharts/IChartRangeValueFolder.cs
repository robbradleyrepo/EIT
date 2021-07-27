namespace LionTrust.Feature.Listings.Models.PerformanceCharts
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    [SitecoreType(TemplateId = Constants.ChartRangeValueFolder.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface IChartRangeValueFolder : IListingsGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IChartRangeValue> ChartRanges { get; set; }
    }
}