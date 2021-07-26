namespace LionTrust.Feature.Fund.Repository
{
    using LionTrust.Foundation.Legacy.Models;
    using System;

    public interface IFundRepository
    {
        FundSearchResultItem GetFundByClass(IFundClass fundClass, string databaseName);

        FundTeamSearchResultItem GetFundTeamByManagerId(Guid fundManagerId, string databaseName);
    }
}
