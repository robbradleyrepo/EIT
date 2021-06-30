namespace LionTrust.Feature.Listings.Models
{
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    public class GenericListingFacets
    {
        public IEnumerable<IListingItemType> ListingItemTypes { get; set; }
    }
}