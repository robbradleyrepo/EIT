namespace LionTrust.Feature.Listings.Models.PerformanceCharts
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IPerformanceChart : IListingsGlassBase
    {
        [SitecoreField(Constants.PerformanceCharts.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.PerformanceCharts.LegendDescription_FieldId)]
        string LegendDescription { get; set; }

        [SitecoreField(Constants.PerformanceCharts.Scale_FieldId)]
        string Scale { get; set; }

        [SitecoreField(Constants.PerformanceCharts.Range_FieldId)]
        string Range { get; set; }

        [SitecoreField(Constants.PerformanceCharts.MaxRange_FieldId)]
        string MaxRange { get; set; }

        [SitecoreField(Constants.PerformanceCharts.Width_FieldId)]
        int Width { get; set; }

        [SitecoreField(Constants.PerformanceCharts.Height_FieldId)]
        int Height { get; set; }

        [SitecoreChildren]
        IEnumerable<IChartColumn> ChartColumns { get; set; }
    }
}