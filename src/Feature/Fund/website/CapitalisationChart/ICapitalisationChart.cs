namespace LionTrust.Feature.Fund.CapitalisationChart
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Fund.Models;
    using System.Collections.Generic;
    public interface ICapitalisationChart : IFundGlassBase
    {
        [SitecoreField(Constants.CapitalisationChart.HeadingFieldId)]
        string Heading { get; set; }
        IEnumerable<ICapitalisationChartEntry> Children { get; set; }
        string JsonDataObject { get; set; }
    }
}
