namespace LionTrust.Feature.Listings.Models.PerformanceCharts
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IPerformanceCharts : IListingsGlassBase
    {
        [SitecoreField(Constants.PerformanceCharts.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.PerformanceCharts.Width_FieldId)]
        int Width { get; set; }

        [SitecoreField(Constants.PerformanceCharts.Height_FieldId)]
        int Height { get; set; }

        [SitecoreChildren]
        IEnumerable<IChartColumnFolder> ChartColumnFolder { get; set; }

        [SitecoreChildren]
        IEnumerable<IChartRangeValueFolder> ChartRangeValueFolder { get; set; }
    }
}