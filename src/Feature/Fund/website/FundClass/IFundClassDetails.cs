using LionTrust.Foundation.Legacy.Models;

namespace LionTrust.Feature.Fund.FundClass
{
    public interface IFundClassDetails
    {
        FundClassData GetFundClassDetails(IFundClass fundClass);
    }
}