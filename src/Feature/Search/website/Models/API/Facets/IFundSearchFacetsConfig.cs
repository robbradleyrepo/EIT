namespace LionTrust.Feature.Search.Models.API.Facets
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFundListingFacetsConfig : ISearchGlassBase
    {
        [SitecoreField(FieldId = Constants.APIFacets.FundSearchFacetsConfig.FundRangesFieldId)]
        IFacetFolder FundRangesFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.FundSearchFacetsConfig.FundRangesLabelFieldId)]
        string FundRangesLabel { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.FundSearchFacetsConfig.FundManagersFieldId)]
        IFundManagerFacetFolder FundManagersFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.FundSearchFacetsConfig.FundManagersLabelFieldId)]
        string FundManagersLabel { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.FundSearchFacetsConfig.FundTeamsFieldId)]
        IFacetFolder FundTeamsFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.FundSearchFacetsConfig.FundTeamsLabelFieldId)]
        string FundTeamsLabel { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.FundSearchFacetsConfig.FundRegionsFieldId)]
        IFacetFolder FundRegionsFolder { get; set; }

        [SitecoreField(FieldId = Constants.APIFacets.FundSearchFacetsConfig.FundRegionsLabelFieldId)]
        string FundRegionsLabel { get; set; }
    }
}