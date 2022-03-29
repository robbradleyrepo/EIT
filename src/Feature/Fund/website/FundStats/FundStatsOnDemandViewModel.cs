namespace LionTrust.Feature.Fund.FundStats
{
    using LionTrust.Feature.Fund.Models;

    public class FundStatsOnDemandViewModel
    {
        public IFourFundStatsOnDemand FundSelector { get; set; }
        public FundStatsDataOnDemand FundValues { get; set; }
    }
}