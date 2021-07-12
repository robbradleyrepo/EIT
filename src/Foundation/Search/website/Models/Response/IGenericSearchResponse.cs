namespace LionTrust.Foundation.Search.Models.Response
{
    using System.Collections.Generic;

    public interface IGenericSearchResponse
    {
        IEnumerable<IGenericContentResult> SearchResults { get; set; }

        int StatusCode { get; set; }

        string StatusMessage { get; set; }

        int TotalResults { get; set; }
    }
}