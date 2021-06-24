namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using System.Collections.Generic;

    public interface IGenericListingModule : IListingsGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IGenericListingModuleItem> ListingItems { get; set; }

        [SitecoreField(Constants.GenericListingModule.Title_FieldID)]
        string Title { get; set; }
    }
}