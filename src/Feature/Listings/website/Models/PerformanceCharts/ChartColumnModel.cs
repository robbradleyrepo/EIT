namespace LionTrust.Feature.Listings.Models.PerformanceCharts
{
    using System.Collections.Generic;
    
    public class ChartColumnModel
    {
        public string Label { get; set; }
        public string Color { get; set; }
        public List<int> Data { get; set; }
    }
}