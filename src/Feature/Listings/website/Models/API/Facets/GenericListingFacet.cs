namespace LionTrust.Feature.Listings.Models.API.Facets
{
    using System.Collections.Generic;
    using LionTrust.Foundation.Legacy.Models;
    
    public class GenericListingFacet
    {
        public string Name { get; set; }
        public IEnumerable<ListingFilterFacetsModel> Items { get; set; }
    }
}