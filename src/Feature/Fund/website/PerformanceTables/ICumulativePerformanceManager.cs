namespace LionTrust.Feature.Fund.PerformanceTables
{
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    public interface ICumulativePerformanceManager
    {
        IEnumerable<PerformanceTableRow> GetPerformanceTableRows(string citiCode, IFundClass fundClassItem);

        PerformanceTableRow GetQuartile(string citiCode, IFundClass fundClassItem);

        string GetDisclaimer(string citiCode, string currency = "", string disclaimerText = "");
    }
}
