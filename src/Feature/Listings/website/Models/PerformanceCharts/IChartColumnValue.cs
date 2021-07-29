namespace LionTrust.Feature.Listings.Models.PerformanceCharts
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Design;

    public interface IChartColumnValue : IListingsGlassBase
    {
        [SitecoreField(Listings.Constants.ChartColumnValue.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Listings.Constants.ChartColumnValue.Value_FieldId)]
        int Value { get; set; }

        [SitecoreField(Listings.Constants.ChartColumnValue.Color_FieldId)]
        ILookupValue Color { get; set; }
    }
}