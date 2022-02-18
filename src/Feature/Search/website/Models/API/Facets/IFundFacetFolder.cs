namespace LionTrust.Feature.Search.Models.API.Facets
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFundFacetFolder : ISearchGlassBase
    {
        [SitecoreChildren(TemplateId = Constants.APIFacets.FundFacetOption.TemplateId)]
        IEnumerable<IFundFacetOption> Children { get; set; }
    }
}