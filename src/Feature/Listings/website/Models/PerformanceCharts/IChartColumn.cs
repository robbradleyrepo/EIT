namespace LionTrust.Feature.Listings.Models.PerformanceCharts
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = Constants.ChartColumns.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface IChartColumn : IListingsGlassBase
    {
        [SitecoreField(Constants.ChartColumns.Title_FieldId)]
        string Title { get; set; }

        [SitecoreChildren]
        IEnumerable<IChartColumnValue> ChartValues { get; set; }
    }
}