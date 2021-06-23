namespace LionTrust.Feature.Search.Models.API.Facets
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFundListingFacetsConfig : ISearchGlassBase
    {
        [SitecoreField(FieldId = Constants.APIFacets.FundSearchFacetsConfig.FundRangesFieldId)]
        IFacetFolder FundRangesFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.FundSearchFacetsConfig.FundManagersFieldId)]
        IFundManagerFacetFolder FundManagersFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.FundSearchFacetsConfig.FundTeamsFieldId)]
        IFacetFolder FundTeamsFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.FundSearchFacetsConfig.FundRegionsFieldId)]
        IFacetFolder FundRegionsFolder { get; set; }
    }
}