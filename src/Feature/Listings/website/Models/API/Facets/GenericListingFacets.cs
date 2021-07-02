namespace LionTrust.Feature.Listings.Models.API.Facets
{
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    public class GenericListingFacets
    {
        public IEnumerable<ListingFilterFacetsModel> ListingItemTypes { get; set; }
        public IEnumerable<ListingFilterFacetsModel> Years { get; set; }
        public IEnumerable<ListingFilterFacetsModel> Months { get; set; }
    }
}