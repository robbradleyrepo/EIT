namespace LionTrust.Feature.Search.Models.API.Response
{
    using Newtonsoft.Json;

    public class FundFacetsResponse
    {
        public FundFacets Facets { get; set; }

        [JsonIgnore]
        public string Message { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}