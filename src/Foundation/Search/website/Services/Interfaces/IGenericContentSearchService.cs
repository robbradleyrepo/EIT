namespace LionTrust.Foundation.Search.Services.Interfaces
{
    using LionTrust.Foundation.Search.Models.ContentSearch;
    using LionTrust.Foundation.Search.Models.Request;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IGenericContentSearchService
    {
        GenericSearchResults GetTaxonomyRelatedGenericItems(GenericSearchRequest genericListingSearchRequest);
    }
}