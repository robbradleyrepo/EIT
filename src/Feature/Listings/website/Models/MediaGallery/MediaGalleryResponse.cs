namespace LionTrust.Feature.Listings.Models
{
    using System.Collections.Generic;

    public class MediaGalleryResponse
    {
        public IEnumerable<MediaItemResponseModel> SearchResults { get; set; }

        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public int TotalResults { get; set; }
    }
}