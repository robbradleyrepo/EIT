namespace LionTrust.Feature.Search.Models.API.Response
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class FacetsResponse
    {
        public IEnumerable<Facet> Facets { get; set; }

        [JsonIgnore]
        public string Message { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}