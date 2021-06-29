namespace LionTrust.Feature.Listings.Models
{
    using Newtonsoft.Json;

    public class DocumentModel
    {
        [JsonProperty("ritle")]
        public string Title { get; set; }

        [JsonProperty("documentLink")]
        public string DocumentLink { get; set; }

        [JsonProperty("documentLinkText")]
        public string DocumentLinkText { get; set; }

        [JsonProperty("documentPageLink")]
        public string DocumentPageLink { get; set; }

        [JsonProperty("documentVideoLink")]
        public string DocumentVideoLink { get; set; }
    }
}