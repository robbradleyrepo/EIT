namespace LionTrust.Feature.Listings.Models
{
    using System.Collections.Generic;

    public class DocumentsResponse
    {
        public IEnumerable<DocumentModel> SearchResults { get; set; }

        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public int TotalResults { get; set; }
    }
}