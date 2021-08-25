namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    
    public interface ILatestResultsAndPresentations : IListingsGlassBase
    {
        [SitecoreField(Constants.LatestResultsAndPresentation.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.TitleColor_FieldId)]
        Foundation.Design.ILookupValue TitleColor { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.ViewAllLabel_FieldId)]
        string ViewAllLabel { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.AllResultsPage_FieldId)]
        IListingPageWithChildren AllResultsPage { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.FirstImage_FieldId)]
        Image FirstImage { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.FirstImageOpacity_FieldId)]
        string FirstImageOpacity { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.SecondImage_FieldId)]
        Image SecondImage { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.SecondImageOpacity_FieldId)]
        string SecondImageOpacity { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.ThirdImage_FieldId)]
        Image ThirdImage { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.ThirdImageOpacity_FieldId)]
        string ThirdImageOpacity { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.PressReleaseIcon_FieldId)]
        Image PressReleaseIcon { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.PressReleaseLabel_FieldId)]
        string PressReleaseLabel { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.ReportIcon_FieldId)]
        Image ReportIcon { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.ReportLabel_FieldId)]
        string ReportLabel { get; set; }

        [SitecoreField(Constants.LatestResultsAndPresentation.DateIcon_FieldId)]
        Image DateIcon { get; set; }
    }
}