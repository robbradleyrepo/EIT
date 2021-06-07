namespace LionTrust.Feature.Fund.Api
{
    using System.Collections.Generic;

    public interface IFundHoldingsBreakdown
    {
        IEnumerable<FundBreakdownModel> GetFundHoldings(string citiCode);
    }
}
