namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.Search.Models;
    using System.Collections.Generic;

    public interface IGenericListingFilters : IGenericListingModule, IListingsGlassBase
    {
        [SitecoreField(FieldId = Constants.GenericListingFolder.YearsList_FieldId)]
        IEnumerable<IFacetItem> Years { get; set; }

        [SitecoreField(FieldId = Constants.GenericListingFolder.MonthsList_FieldId)]
        IEnumerable<IFacetItem> Months { get; set; }
    }
}