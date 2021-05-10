namespace LionTrust.Feature.Search.Models.API.Facets
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IFacetFolder : ISearchGlassBase
    {
        [SitecoreChildren]
        IEnumerable<ISearchGlassBase> Children { get; set; }
    }
}