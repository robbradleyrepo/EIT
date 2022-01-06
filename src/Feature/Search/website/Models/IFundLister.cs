namespace LionTrust.Feature.Search.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;

    public interface IFundLister : IPagination, ISearchGlassBase
    {
        [SitecoreField(Constants.FundLister.ApplyFiltersLabel_FieldId)]
        string ApplyFiltersLabel { get; set; }

        [SitecoreField(Constants.FundLister.ClearFiltersLabel_FieldId)]
        string ClearFiltersLabel { get; set; }

        [SitecoreField(Constants.FundLister.ViewAsListLabel_FieldId)]
        string ViewAsListLabel { get; set; }

        [SitecoreField(Constants.FundLister.ViewAsGridLabel_FieldId)]
        string ViewAsGridLabel { get; set; }

        [SitecoreField(Constants.FundLister.FiltersLabel_FieldId)]
        string FiltersLabel { get; set; }

        [SitecoreField(Constants.FundLister.FundCentreLink_FieldId)]
        Link FundCentreLink { get; set; }

        [SitecoreField(Constants.FundLister.SearchPlaceholder_FieldId)]
        string SearchPlaceholder { get; set; }

        [SitecoreField(Constants.FundLister.MobileViewLabel_FieldId)]
        string MobileViewLabel { get; set; }

        [SitecoreField(Constants.FundLister.FundLiterature_FieldId)]
        IGlassBase FundLiterature { get; set; }

        [SitecoreField(Constants.FundLister.AllDocumentsLabel_FieldId)]
        string AllDocumentsLabel { get; set; }

        [SitecoreField(Constants.FundLister.FollowLabel_FieldId)]
        string FollowLabel { get; set; }

        [SitecoreField(Constants.FundLister.FactsheetLabel_FieldId)]
        string FactsheetLabel { get; set; }
    }
}