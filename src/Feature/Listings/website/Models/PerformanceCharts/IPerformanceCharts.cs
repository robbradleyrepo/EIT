namespace LionTrust.Feature.Listings.Models.PerformanceCharts
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IPerformanceCharts : IListingsGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IPerformanceChart> PerformanceCharts { get; set; }
    }
}