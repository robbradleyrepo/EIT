namespace LionTrust.Feature.Search.Models.API.Response
{
    using System.Collections.Generic;

    public class SiteFacetsResponse
    {
        public IEnumerable<FacetItem> FundFacets { get; set; }

        public IEnumerable<FacetItem> FundCategoriesFacets { get; set; }

        public IEnumerable<FacetItem> FundManagersFacets { get; set; }

        public IEnumerable<FacetItem> FundTeamsFacets { get; set; }

        public string Message { get; set; }

        public int StatusCode { get; set; }
    }
}