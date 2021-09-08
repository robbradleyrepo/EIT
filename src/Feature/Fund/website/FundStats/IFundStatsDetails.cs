using LionTrust.Foundation.Legacy.Models;

namespace LionTrust.Feature.Fund.FundStats
{
    public interface IFundStatsDetails
    {
        FundStatsData GetFundStatsDetails(IFundClass fundClass);
    }
}