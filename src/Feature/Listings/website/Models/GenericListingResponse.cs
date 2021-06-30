namespace LionTrust.Feature.Listings.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class GenericListingResponse
    {
        public IEnumerable<GenericListingModuleItemSearchResult> SearchResults { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        [JsonIgnore]
        public string StatusMessage { get; set; }

        public int TotalResults { get; set; }
    }
}