namespace LionTrust.Feature.Search.Models.API
{
    using System.Collections.Generic;

    public partial class ArticleFacets
    {
        public IEnumerable<FacetItem> Funds { get; set; }

        public IEnumerable<FacetItem> FundCategories { get; set; }

        public IEnumerable<FacetItem> FundManagers { get; set; }

        public IEnumerable<FacetItem> FundTeams { get; set; }
    }
}