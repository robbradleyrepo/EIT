namespace LionTrust.Feature.Fund.Models
{
    using LionTrust.Feature.Fund.Api;
    using System.Collections.Generic;

    public class GraphBreakdownViewModel
    {
        public IGraphWithHeading Component { get; set; }

        public IEnumerable<FundBreakdownModel> Breakdown { get; set; }
    }
}