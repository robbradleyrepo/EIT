namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System.Collections.Generic;

    public interface IListingPageWithChildren : IListingsGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IListingPageWithChildren> Children { get; set; }
    }
}