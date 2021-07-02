namespace LionTrust.Feature.Listings.Models.API.Response
{
    using LionTrust.Feature.Listings.Models.API.Facets;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class GenericListerFacetResponse
    {
        public GenericListingFacets Facets { get; set; }

        [JsonIgnore]
        public string Message { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}