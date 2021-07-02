namespace LionTrust.Foundation.Legacy.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System.Collections.Generic;

    public interface IGenericListingModule : ILegacyGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IGenericListingModuleItem> ListingItems { get; set; }

        [SitecoreField(Constants.GenericListingModule.Title_FieldID)]
        string Title { get; set; }

        [SitecoreField(Constants.GenericListingModule.ListingTypeList_FieldId)]
        IEnumerable<IListingItemType> ListingTypeList { get; set; }
    }
}