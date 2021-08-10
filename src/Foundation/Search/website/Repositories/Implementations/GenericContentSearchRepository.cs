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

    public class GenericContentSearchRepository : IGenericContentSearchRepository
    {
        // Doesn't need facet counts initially
        public ContentSearchResults<GenericSearchResultItem> GetGenericSearchResultItems(Expression<Func<GenericSearchResultItem, bool>> predicate, int skip, int take, string database = "web")
        {
            using (IProviderSearchContext context = ContentSearchManager
                                                            .GetIndex($"liontrust_generic_{database}_index")
                                                            .CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
            {
                var query = context.GetQueryable<GenericSearchResultItem>()
                                 .Where(predicate);
                
                var results = query.Skip(skip).Take(take).GetResults();
                
                if (results == null)
                {
                    return null;
                }

                return new ContentSearchResults<GenericSearchResultItem> { SearchResults = results, TotalResults = results.TotalSearchResults };
            }
        }
    }
}