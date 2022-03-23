namespace LionTrust.Foundation.Search.Repositories.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using LionTrust.Foundation.Search.Models.ContentSearch;

    public interface IFundContentSearchRepository
    {
        ContentSearchResults<FundSearchResultItem> GetFundSearchResultItems(Expression<Func<FundSearchResultItem, bool>> predicate, int skip, int take, string database, Func<IQueryable<FundSearchResultItem>, IQueryable<FundSearchResultItem>> sort);

        ContentSearchResults<FundSearchResultItem> GetAllFundSearchResultItems(Expression<Func<FundSearchResultItem, bool>> predicate, string database = "web");
    }
}