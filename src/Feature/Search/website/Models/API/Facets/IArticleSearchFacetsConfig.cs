namespace LionTrust.Feature.Search.Models.API.Facets
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IArticleListingFacetsConfig : ISearchGlassBase
    {
        [SitecoreField(FieldId = Constants.APIFacets.ArticleSearchFacetsConfig.FundsFieldId)]
        IFundFacetFolder FundsFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.ArticleSearchFacetsConfig.FundsLabelFieldId)]
        string FundsLabel { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.ArticleSearchFacetsConfig.CategoriesFieldId)]
        IFacetFolder CategoriesFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.ArticleSearchFacetsConfig.CategoriesLabelFieldId)]
        string CategoriesLabel { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.ArticleSearchFacetsConfig.FundManagersFieldId)]
        IFundManagerFacetFolder FundManagersFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.ArticleSearchFacetsConfig.FundManagersLabelFieldId)]
        string FundManagersLabel { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.ArticleSearchFacetsConfig.FundTeamsFieldId)]
        IFacetFolder FundTeamsFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.ArticleSearchFacetsConfig.FundTeamsLabelFieldId)]
        string FundTeamsLabel { get; set; }
    }
}