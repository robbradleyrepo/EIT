namespace LionTrust.Feature.Listings.Models.PerformanceCharts
{
    public class PerformanceChartsViewModel
    {
        public IPerformanceCharts Data { get; set; }
        public string ChartJson { get; set; }
        public IChartColumnFolder ChartColumnFolder { get; set; }
        public IChartRangeValueFolder ChartRangeValueFolder { get; set; }
    }
}