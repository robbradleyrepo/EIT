namespace LionTrust.Feature.Fund.FundStats
{
    using LionTrust.Feature.Fund.Models;

    public class FundStatsViewModel
    {
        public IFourFundStats FundSelector { get; set; }
        public FundStatsData FundValues { get; set; }
    }
}