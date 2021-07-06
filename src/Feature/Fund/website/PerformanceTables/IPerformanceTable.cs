namespace LionTrust.Feature.Fund.PerformanceTables
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Fund.Models;

    public interface IPerformanceTable: IFundSelector
    {
        [SitecoreField(Constants.PerformanceTable.TableHeadingFieldId)]
        string HeadingLabel { get; set; }

        [SitecoreField(Constants.PerformanceTable.QuartileRowLabelFieldId)]
        string QuartileRowLabel { get; set; }

        [SitecoreField(Constants.PerformanceTable.ColumnMonthPrefixFieldId)]
        string ColumnMonth { get; set; }

        [SitecoreField(Constants.PerformanceTable.HeadingFieldId)]
        string Heading { get; set; }
    }
}
