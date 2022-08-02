namespace LionTrust.Foundation.Search.Repositories.Implementations
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Repositories.Interfaces;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.Security;

    public class FundContentSearchRepository : IFundContentSearchRepository
    {
        // Doesn't need facet counts initially
        public ContentSearchResults<FundSearchResultItem> GetFundSearchResultItems(Expression<Func<FundSearchResultItem, bool>> predicate, int skip, int take, string database = "web", Func<IQueryable<FundSearchResultItem>, IQueryable<FundSearchResultItem>> sort = null)
        {
            using (IProviderSearchContext context = ContentSearchManager
                                                            .GetIndex($"liontrust_fund_{database}_index")
                                                            .CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
            {
                var query = context.GetQueryable<FundSearchResultItem>()
                                 .Where(predicate);
                
                if (sort != null)
                {
                    query = sort(query);
                }
                
                var results = query.Skip(skip).Take(take).GetResults();
                
                if (results == null)
                {
                    return null;
                }

                return new ContentSearchResults<FundSearchResultItem> 
                { 
                    SearchResults = results, 
                    TotalResults = results.TotalSearchResults
                };
            }
        }

        public ContentSearchResults<FundSearchResultItem> GetAllFundSearchResultItems(Expression<Func<FundSearchResultItem, bool>> predicate, string database = "web")
        {
            using (IProviderSearchContext context = ContentSearchManager
                                                            .GetIndex($"liontrust_fund_{database}_index")
                                                            .CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
            {
                var query = context.GetQueryable<FundSearchResultItem>()
                                 .Where(predicate);

                var results = query.GetResults();

                if (results == null)
                {
                    return null;
                }

                return new ContentSearchResults<FundSearchResultItem> 
                { 
                    SearchResults = results, 
                    TotalResults = results.TotalSearchResults
                };
            }
        }

        public FundTeamFacetsSearchResults GetFundTeamFacets(Expression<Func<FundSearchResultItem, bool>> predicate, string database = "web")
        {
            using (IProviderSearchContext context = ContentSearchManager
                                                            .GetIndex($"liontrust_fund_{database}_index")
                                                            .CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
            {
                var query = context.GetQueryable<FundSearchResultItem>()
                                 .Where(predicate).FacetOn(x => x.FundTeam, 1);
              
                var results = query.GetResults();

                if (results == null)
                {
                    return null;
                }

                var fundTeamsFacetCategory = results.Facets?.Categories?.FirstOrDefault(f => f.Name == "legacyfund_fundteam");

                return new FundTeamFacetsSearchResults
                {
                    FacetValues = fundTeamsFacetCategory?.Values
                };
            }
        }
    }
}