namespace LionTrust.Feature.Listings.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IGenericListingItemTypeFolder
    {
        [SitecoreChildren(TemplateId = Foundation.Legacy.Constants.ListingItemType.TemplateId)]
        IEnumerable<Foundation.Legacy.Models.IListingItemType> Children { get; set; }
    }
}