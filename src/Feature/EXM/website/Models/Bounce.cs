using Newtonsoft.Json;

namespace LionTrust.Feature.EXM.Models
{
    [JsonObject]
    public class Bounce
    {
        [JsonProperty("created")]
        public int Created { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public bool HardBounce => !string.IsNullOrWhiteSpace(Status) && Status.StartsWith("5");
    }
}