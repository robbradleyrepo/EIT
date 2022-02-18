namespace LionTrust.Feature.Search.Models.API.Facets
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    [SitecoreType(TemplateId = Constants.APIFacets.FundFacetOption.TemplateId)]
    public interface IFundFacetOption : ISearchGlassBase
    {
        [SitecoreField(FieldId = Constants.APIFacets.FundFacetOption.HideFromFiltersFieldId)]
        bool HideFromFilters { get; set; }
    }
}