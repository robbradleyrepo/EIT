namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IGenericListingFacetsConfig : IListingsGlassBase
    {
        [SitecoreField(FieldId = Constants.GenericListingFacetsConfig.ListingItemTypeFolder_FieldId)]
        IGenericListingItemTypeFolder ListingItemTypeFolder { get; set; }
    }
}