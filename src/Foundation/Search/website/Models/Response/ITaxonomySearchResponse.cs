namespace LionTrust.Foundation.Search.Models.Response
{
    using System.Collections.Generic;

    public interface ITaxonomySearchResponse
    {        
        IEnumerable<ITaxonomyContentResult> SearchResults { get; set; }

        int StatusCode { get; set; }

        string StatusMessage { get; set; }

        int TotalResults { get; set; }
    }
}