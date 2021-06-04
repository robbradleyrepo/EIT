namespace LionTrust.Feature.Fund.Repository
{
    using LionTrust.Foundation.Legacy.Models;

    public interface IFundRepository
    {
        FundSearchResultItem GetFundByClass(IFundClass fundClass, string databaseName);
    }
}
