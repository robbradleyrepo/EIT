namespace LionTrust.Feature.Fund.PerformanceTables
{
    using System.Collections.Generic;

    public interface IDiscretePerformanceManager
    {
        IEnumerable<PerformanceTableRow> GetPerformanceTableRows(string citiCode);

        PerformanceTableRow GetQuartile(string citiCode);
    }
}
