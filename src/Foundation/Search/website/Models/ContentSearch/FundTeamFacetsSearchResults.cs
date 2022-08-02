namespace LionTrust.Foundation.Search.Models.ContentSearch
{
    using Sitecore.ContentSearch.Linq;
    using System.Collections.Generic;

    public class FundTeamFacetsSearchResults
    {      
        public IEnumerable<FacetValue> FacetValues { get; set; }
    }
}