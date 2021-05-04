namespace LionTrust.Feature.Search.Models.API.Response
{
    using System.Collections.Generic;

    using LionTrust.Foundation.Search.Models;
    using LionTrust.Foundation.Search.Models.Response;

    public class ArticleSearchResponse : ITaxonomySearchResponse
    {
        public IEnumerable<ITaxonomyContentResult> SearchResults { get; set; }

        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public int TotalResults { get; set; }
    }
}