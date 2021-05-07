namespace LionTrust.Feature.Fund.Models
{
    using LionTrust.Foundation.Legacy.Models;

    public class FundOverviewViewModel
    {
        public IFund FundPage { get; set; }
        public IFundOverview OverviewComponent { get; set; }
    }
}