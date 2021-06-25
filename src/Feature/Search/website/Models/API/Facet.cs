namespace LionTrust.Feature.Search.Models.API
{
    using System.Collections.Generic;

    public partial class Facet
    {
        public string Name { get; set; }

        public IEnumerable<FacetItem> Items { get; set; }
    }
}