namespace LionTrust.Feature.Fund.PerformanceTables
{
    using System.Collections.Generic;

    public interface ICumulativePerformanceManager
    {
        IEnumerable<PerformanceTableRow> GetPerformanceTableRows(string citiCode);

        PerformanceTableRow GetQuartile(string citiCode);

        string GetDisclaimer(string citiCode);
    }
}
