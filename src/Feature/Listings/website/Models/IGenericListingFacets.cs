namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Legacy.Models;
    using LionTrust.Foundation.Search.Models;
    using System.Collections.Generic;

    public interface IGenericListingFacets : IGenericListingModule, IListingsGlassBase
    {
        [SitecoreField(FieldId = Constants.GenericListingFacet.YearsList_FieldId)]
        IEnumerable<IFacetItem> Years { get; set; }

        [SitecoreField(FieldId = Constants.GenericListingFacet.MonthsList_FieldId)]
        IEnumerable<IFacetItem> Months { get; set; }

        [SitecoreField(FieldId = Constants.GenericListingFacet.ApplyFiltersLabel_FieldId)]
        string ApplyFiltersLabel { get; set; }

        [SitecoreField(FieldId = Constants.GenericListingFacet.ClearAllLabel_FieldId)]
        string ClearAllLabel { get; set; }

        [SitecoreField(FieldId = Constants.GenericListingFacet.FilterLabel_FieldId)]
        string FilterLabel { get; set; }

        [SitecoreField(FieldId = Constants.GenericListingFacet.FiltersLabel_FieldId)]
        string FiltersLabel { get; set; }

        [SitecoreField(FieldId = Constants.GenericListingFacet.SearchByKeywordLabel_FieldId)]
        string SearchByKeywordLabel { get; set; }

        [SitecoreField(FieldId = Constants.GenericListingFacet.PreviousLabel_FieldId)]
        string PreviousLabel { get; set; }

        [SitecoreField(FieldId = Constants.GenericListingFacet.NextLabel_FieldId)]
        string NextLabel { get; set; }
    }
}