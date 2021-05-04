namespace LionTrust.Foundation.Search.Services.Interfaces
{
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    
    public interface IArticleContentSearchService
    {
        ContentSearchResults GetDatedTaxonomyRelatedArticles(ITaxonomySearchRequest articleSearchRequest);
    }
}