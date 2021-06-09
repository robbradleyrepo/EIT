namespace LionTrust.Feature.Search.Models.API.Response
{
    using System.Collections.Generic;

    using LionTrust.Foundation.Search.Models;
    using LionTrust.Foundation.Search.Models.Response;
    using Newtonsoft.Json;

    public class ArticleSearchResponse : ITaxonomySearchResponse
    {
        public IEnumerable<ITaxonomyContentResult> SearchResults { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        [JsonIgnore]
        public string StatusMessage { get; set; }
        
        public int TotalResults { get; set; }
    }
}