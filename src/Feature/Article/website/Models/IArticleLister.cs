namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IArticleLister : IArticleGlassBase
    {
        [SitecoreField(Constants.ArticleLister.ApplyFiltersLabel_FieldId)]
        string ApplyFiltersLabel { get; set; }

        [SitecoreField(Constants.ArticleLister.ClearFiltersLabel_FieldId)]
        string ClearFiltersLabel { get; set; }

        [SitecoreField(Constants.ArticleLister.DateNewestFirstLabel_FieldId)]
        string DateNewestFirstLabel { get; set; }

        [SitecoreField(Constants.ArticleLister.DateOldestFirstLabel_FieldId)]
        string DateOldestFirstLabel { get; set; }

        [SitecoreField(Constants.ArticleLister.FiltersLabel_FieldId)]
        string FiltersLabel { get; set; }

        [SitecoreField(Constants.ArticleLister.FundLabel_FieldId)]
        string FundLabel { get; set; }

        [SitecoreField(Constants.ArticleLister.MonthLabel_FieldId)]
        string MonthLabel { get; set; }

        [SitecoreField(Constants.ArticleLister.NextLabel_FieldId)]
        string NextLabel { get; set; }

        [SitecoreField(Constants.ArticleLister.PreviousLabel_FieldId)]
        string PreviousLabel { get; set; }

        [SitecoreField(Constants.ArticleLister.SearchPlaceholder_FieldId)]
        string SearchPlaceholder { get; set; }

        [SitecoreField(Constants.ArticleLister.YearLabel_FieldId)]
        string YearLabel { get; set; }
    }
}