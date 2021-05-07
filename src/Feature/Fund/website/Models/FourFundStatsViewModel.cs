namespace LionTrust.Feature.Fund.Models
{
    using LionTrust.Foundation.Legacy.Models;

    public class FourFundStatsViewModel
    {
        public IFund Fund { get; set; }
        public IFourFundStats FourFundStatsComponent { get; set; }
    }
}