namespace LionTrust.Feature.Listings.Models.API.Response
{
    using System.Collections.Generic;
    using LionTrust.Feature.Listings.Models.API.Facets;
    using LionTrust.Foundation.Legacy.Models;
    using Newtonsoft.Json;
    
    public class GenericListerFacetResponse
    {
        public List<GenericListingFacet> Facets { get; set; }

        [JsonIgnore]
        public string Message { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}