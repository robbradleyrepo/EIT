namespace LionTrust.Feature.Listings.Models.PerformanceCharts
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    [SitecoreType(TemplateId = Constants.ChartColumnFolder.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface IChartColumnFolder : IListingsGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IChartColumn> ChartColumns { get; set; }
    }
}