namespace LionTrust.Foundation.Search.Models.Response
{
    using System.Collections.Generic;

    public interface ISearchResponse<T>
    {        
        IEnumerable<T> SearchResults { get; set; }

        int StatusCode { get; set; }

        string StatusMessage { get; set; }

        int TotalResults { get; set; }

        IEnumerable<string> FacetValues { get; set; }
    }
}