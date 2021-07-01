namespace LionTrust.Feature.Listings.Models.API.Facets
{
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    public class GenericListingFacets
    {
        public IEnumerable<ListingItemTypeModel> ListingItemTypes { get; set; }
    }
}