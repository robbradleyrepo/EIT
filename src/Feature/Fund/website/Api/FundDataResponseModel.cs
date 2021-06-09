namespace LionTrust.Feature.Fund.Api
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class FundDataResponseModel
    {
        [JsonProperty("citicode")]
        public string CitiCode { get; set; }

        [JsonProperty("UnitLaunchDate")]
        public string UnitLaunchDate { get; set; }

        [JsonProperty("Offer")]
        public string OfferPrice { get; set; }

        [JsonProperty("UnitPriceDate")]
        public string UnitPriceDate { get; set; }

        [JsonProperty("Single")]
        public string SinglePrice { get; set; }

        [JsonProperty("Top10Holdings")]
        public Holdings Holdings { get; set; }

        [JsonProperty("SectorBD")]
        public SectorBreakdown SectorBreakdown { get; set; }

        [JsonProperty("InitialCharge")]
        public string InitialCharge { get; set; }

        [JsonProperty("ISIN")]
        public string ISINCode { get; set; }

        [JsonProperty("OngoingCharges")]
        public string OngoingCharges { get; set; }

        [JsonProperty("Sedol")]
        public string SedolCode { get; set; }
    }

    public class SectorBreakdown
    {
        [JsonProperty("SectorBDDate")]
        public string Date { get; set; }

        [JsonProperty("Breakdowns")]
        public Breakdowns Breakdowns { get; set; }
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

