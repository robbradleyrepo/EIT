namespace LionTrust.Feature.Listings.Models.API.Response
{
    using LionTrust.Feature.Listings.Models.API.Facets;
    using LionTrust.Foundation.Search.Models.API;
    using Newtonsoft.Json;

    public class GenericListerFacetResponse
    {
        public GenericListingFacets Facets { get; set; }

        public Dates Dates = new Dates();

        [JsonIgnore]
        public string Message { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}