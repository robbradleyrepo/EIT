﻿namespace LionTrust.Feature.Search.Models.API.Response
{
    using Newtonsoft.Json;

    public class ArticleFacetsResponse
    {
        public ArticleFacets Facets { get; set; }

        [JsonIgnore]
        public string Message { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}