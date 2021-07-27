namespace LionTrust.Feature.Listings.Models.PerformanceCharts
{
    using System.Collections.Generic;

    public class PerformanceChartsViewModel
    {
        public IPerformanceCharts Data { get; set; }
        public string ChartJson { get; set; }
    }
}