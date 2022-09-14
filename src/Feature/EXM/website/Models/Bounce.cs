using Newtonsoft.Json;

namespace LionTrust.Feature.EXM.Models
{
    [JsonObject]
    public class Bounce
    {
        [JsonProperty(Constants.Bounce.Created)]
        public int Created { get; set; }

        [JsonProperty(Constants.Bounce.Email)]
        public string Email { get; set; }

        [JsonProperty(Constants.Bounce.Reason)]
        public string Reason { get; set; }

        [JsonProperty(Constants.Bounce.Status)]
        public string Status { get; set; }

        //5XX errors are hard bounces 
        public bool HardBounce => !string.IsNullOrWhiteSpace(Status) && Status.StartsWith("5");
    }
}