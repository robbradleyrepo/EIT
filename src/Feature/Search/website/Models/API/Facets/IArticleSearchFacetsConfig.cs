namespace LionTrust.Feature.Search.Models.API.Facets
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IArticleListingFacetsConfig : ISearchGlassBase
    {
        [SitecoreField(FieldId = Constants.APIFacets.ArticleSearchFacetsConfig.FundsFieldId)]
        IFacetFolder FundsFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.ArticleSearchFacetsConfig.FundCategoriesFieldId)]
        IFacetFolder FundCategoriesFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.ArticleSearchFacetsConfig.FundManagersFieldId)]
        IFundManagerFacetFolder FundManagersFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.ArticleSearchFacetsConfig.FundTeamsFieldId)]
        IFacetFolder FundTeamsFolder { get; set; }
    }
}