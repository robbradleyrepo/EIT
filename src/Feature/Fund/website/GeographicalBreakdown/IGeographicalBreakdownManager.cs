namespace LionTrust.Feature.Fund.GeographicalBreakdown
{
    using LionTrust.Feature.Fund.Api;
    using System.Collections.Generic;

    public interface IGeographicalBreakdownManager
    {
        IEnumerable<FundBreakdownModel> GetBreakdowns(string citiCode);
    }
}
