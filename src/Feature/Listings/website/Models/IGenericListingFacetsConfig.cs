namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    public interface IGenericListingFacetsConfig : IListingsGlassBase
    {
        [SitecoreField(FieldId = Constants.GenericListingFolder.ListingTypeList_FieldId)]
        IEnumerable<IListingItemType> ListingTypeList { get; set; }
    }
}