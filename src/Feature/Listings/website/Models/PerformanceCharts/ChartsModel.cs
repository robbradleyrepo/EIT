namespace LionTrust.Feature.Listings.Models.PerformanceCharts
{
    using System.Collections.Generic;

    public class ChartsModel
    {
        public List<string> Labels { get; set; }       
        public List<ChartColumnModel> Datasets { get; set; }
        public YAxeConfig YAxeConfig { get; set; }
    }
}