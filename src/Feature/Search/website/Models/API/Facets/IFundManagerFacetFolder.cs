namespace LionTrust.Feature.Search.Models.API.Facets
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFundManagerFacetFolder : ISearchGlassBase
    {
        [SitecoreChildren(TemplateId = Constants.APIFacets.FundManagerFacetOption.TemplateId, InferType = true)]
        IEnumerable<IFundManagerFacetOption> Children { get; set; }
    }
}