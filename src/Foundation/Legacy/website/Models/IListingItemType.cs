namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy;

    public interface IListingItemType : ILegacyGlassBase
    {
        [SitecoreField(Constants.ListingItemType.ListingItemTypeName_FieldId)]
        string ListingItemTypeName { get; set; }
    }
}