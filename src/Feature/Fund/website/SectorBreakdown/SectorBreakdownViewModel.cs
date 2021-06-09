namespace LionTrust.Feature.Fund.SectorBreakdown
{
    using LionTrust.Feature.Fund.Api;
    using System.Collections.Generic;

    public class SectorBreakdownViewModel
    {
        public ISectorBreakdown Component { get; set; }

        public IEnumerable<FundBreakdownModel> Breakdown { get; set; }
    }
}