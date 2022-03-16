namespace LionTrust.Feature.Fund.PerformanceTables
{
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    public interface IDiscretePerformanceManager
    {
        IEnumerable<PerformanceTableRow> GetPerformanceTableRows(string citiCode, IFundClass fundClassItem);

        PerformanceTableRow GetQuartile(string citiCode, IFundClass fundClassItem);

        string[] GetColumnHeadings(string citiCode);
    }
}
