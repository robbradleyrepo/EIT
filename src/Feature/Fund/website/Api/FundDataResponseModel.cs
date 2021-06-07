namespace LionTrust.Feature.Fund.Api
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class FundDataResponseModel
    {
        [JsonProperty("citicode")]
        public string Citicode { get; set; }

        [JsonProperty("Top10Holdings")]
        public Holdings Holdings { get; set; }
    }

    public class Holdings
    {
        [JsonProperty("Top10HoldingsDate")]
        public string Date { get; set; }

        [JsonProperty("Breakdowns")]
        public Breakdowns Breakdowns { get; set; }
    }
    public class Breakdowns
    {
        [JsonProperty("Breakdown")]
        public IEnumerable<Breakdown> Data { get; set; }
    }

    public class Breakdown
    {
        [JsonProperty("BreakdownName")]
        public string Name { get; set; }

        [JsonProperty("BreakdownWeight")]
        public string Weight { get; set; }
    }
}

