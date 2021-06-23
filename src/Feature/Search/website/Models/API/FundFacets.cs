namespace LionTrust.Feature.Search.Models.API
{
    using System.Collections.Generic;

    public partial class FundFacets
    {
        public IEnumerable<FacetItem> FundManagers { get; set; }

        public IEnumerable<FacetItem> FundTeams { get; set; }

        public IEnumerable<FacetItem> FundRegions { get; set; }

        public IEnumerable<FacetItem> FundRanges { get; set; }
    }
}