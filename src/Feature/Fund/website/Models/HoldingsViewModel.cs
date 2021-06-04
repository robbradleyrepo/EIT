namespace LionTrust.Feature.Fund.Models
{
    using LionTrust.Feature.Fund.Api;
    using System.Collections.Generic;

    public class HoldingsViewModel
    {
        public IEnumerable<FundBreakdownModel> Holdings { get; set; }

        public IHoldingsTable Table { get; set; }
    }
}