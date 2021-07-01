namespace LionTrust.Foundation.Legacy.Models
{
    using Newtonsoft.Json;

    public class ListingItemTypeModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }
    }
}