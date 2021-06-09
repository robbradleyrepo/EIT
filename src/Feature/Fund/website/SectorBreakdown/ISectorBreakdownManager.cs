namespace LionTrust.Feature.Fund.SectorBreakdown
{
    using LionTrust.Feature.Fund.Api;
    using System.Collections.Generic;

    public interface ISectorBreakdownManager
    {
        IEnumerable<FundBreakdownModel> GetFundClassBreakdowns(string citiCode);
    }
}
