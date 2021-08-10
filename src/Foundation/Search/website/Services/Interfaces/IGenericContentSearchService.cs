namespace LionTrust.Foundation.Search.Services.Interfaces
{
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;

    public interface IGenericContentSearchService
    {
        ContentSearchResults<GenericSearchResultItem> GetTaxonomyRelatedGenericItems(GenericSearchRequest genericListingSearchRequest);
    }
}