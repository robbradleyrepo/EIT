namespace LionTrust.Foundation.Search.Services.Interfaces
{
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using System;
    using System.Linq;

    public interface IFundContentSearchService
    {
        ContentSearchResults<FundSearchResultItem> GetFunds(FundSearchRequest fundSearchRequest, Func<IQueryable<FundSearchResultItem>, IQueryable<FundSearchResultItem>> sort = null);

        ContentSearchResults<FundSearchResultItem> GetAllAllowedFunds();

        FundTeamFacetsSearchResults GetFundTeamFacets(FundSearchRequest fundSearchRequest);
    }
}