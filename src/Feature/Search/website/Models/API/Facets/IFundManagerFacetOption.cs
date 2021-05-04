namespace LionTrust.Feature.Search.Models.API.Facets
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    [SitecoreType(TemplateId = Constants.APIFacets.FundManagerFacetOption.TemplateId)]
    public interface IFundManagerFacetOption : ISearchGlassBase
    {
        [SitecoreField(FieldId = Constants.APIFacets.FundManagerFacetOption.IsFundManagerFieldId)]
        bool IsFundManager { get; set; }
    }
}