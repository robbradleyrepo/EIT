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

    public class ArticleContentSearchRepository : IArticleContentSearchRepository
    {
        // Doesn't need facet counts initially
        public ContentSearchResults GetArticleSearchResultItems(Expression<Func<ArticleSearchResultItem, bool>> predicate, int skip, int take, string database = "web", Func<IQueryable<ArticleSearchResultItem>, IQueryable<ArticleSearchResultItem>> sort = null)
        {
            using (IProviderSearchContext context = ContentSearchManager
                                                            .GetIndex($"liontrust_article_{database}_index")
                                                            .CreateSearchContext(SearchSecurityOptions.DisableSecurityCheck))
            {
                var query = context.GetQueryable<ArticleSearchResultItem>()
                                 .Where(predicate);
                
                if (sort != null)
                {
                    query = sort(query);
                }
                
                var results = query.GetResults();
                
                if (results == null)
                {
                    return null;
                }

                return new ContentSearchResults { SearchResults = results.Hits.Skip(skip).Take(take), TotalResults = results.TotalSearchResults };
            }
        }
    }
}