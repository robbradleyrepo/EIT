namespace LionTrust.Feature.Search.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Feature.Search.Models.API.Facets;
    using LionTrust.Foundation.ORM.Models;

    public interface IMyFundsLister : IPagination, ISearchGlassBase
    {
        [SitecoreField(Constants.MyFundsLister.ApplyFiltersLabel_FieldId)]
        string ApplyFiltersLabel { get; set; }

        [SitecoreField(Constants.MyFundsLister.ClearFiltersLabel_FieldId)]
        string ClearFiltersLabel { get; set; }

        [SitecoreField(Constants.MyFundsLister.FiltersLabel_FieldId)]
        string FiltersLabel { get; set; }

        [SitecoreField(Constants.MyFundsLister.FundLiterature_FieldId)]
        IGlassBase FundLiterature { get; set; }

        [SitecoreField(Constants.MyFundsLister.AllDocumentsCtaText_FieldId)]
        string AllDocumentsLabel { get; set; }

        [SitecoreField(Constants.MyFundsLister.FundUpdateCtaText_FieldId)]
        string UpdateFundCtaText { get; set; }

        [SitecoreField(Constants.MyFundsLister.ViewFundCtaText_FieldId)]
        string ViewFundCtaText { get; set; }

        [SitecoreField(Constants.MyFundsLister.SortLabel_FieldId)]
        string SortLabel { get; set; }

        [SitecoreField(Constants.MyFundsLister.FacetsConfiguration_FieldId)]
        IFundListingFacetsConfig FacetsConfiguration { get; set; }

        [SitecoreField(Constants.MyFundsLister.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.MyFundsLister.SortAZLabel_FieldId)]
        string SortAZLabel { get; set; }

        [SitecoreField(Constants.MyFundsLister.SortZALabel_FieldId_FieldId)]
        string SortZALabel { get; set; }
    }
}