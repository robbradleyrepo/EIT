namespace LionTrust.Feature.Fund.CapitalisationChart
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Fund.Models;
   public interface ICapitalisationChartEntry: IFundGlassBase
    {
        [SitecoreField(Constants.CapitalisationChartEntry.NameFieldId)]
        string RowName { get; set; }

        [SitecoreField(Constants.CapitalisationChartEntry.ValueFieldId)]
        string Value { get; set; }

        [SitecoreField(Constants.CapitalisationChartEntry.BackgroundColorFieldId)]
        string BackgroundColor { get; set; }
    }
}
