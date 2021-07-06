namespace LionTrust.Feature.Fund.Api
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class FundDataResponseModel
    {
        [JsonProperty("Discrete0To12mQuart_QE")]
        public string DiscreteQuartile0To12m { get; set; }

        [JsonProperty("Discrete12mTo24mQuart_QE")]
        public string DiscreteQuartile12mTo24m { get; set; }

        [JsonProperty("Discrete24mTo36mQuart_QE")]
        public string DiscreteQuartile24mTo36m { get; set; }

        [JsonProperty("Discrete36mTo48mQuart_QE")]
        public string DiscreteQuartile36mTo48m { get; set; }

        [JsonProperty("Discrete48mTo60mQuart_QE")]
        public string DiscreteQuartile48mTo60m { get; set; }

        [JsonProperty("FundNameLong")]
        public string FundName { get; set; }

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

        [JsonProperty("RegionBD")]
        public RegionBreakdown RegionBreakdown { get; set; }

        [JsonProperty("InitialCharge")]
        public string InitialCharge { get; set; }

        [JsonProperty("ISIN")]
        public string ISINCode { get; set; }

        [JsonProperty("OngoingCharge")]
        public string OngoingCharge { get; set; }

        [JsonProperty("Sedol")]
        public string SedolCode { get; set; }

        [JsonProperty("Discrete0To12m_YE")]
        public string DiscretePerformance0To12 { get; set; }

        [JsonProperty("Discrete12mTo24m_YE")]
        public string DiscretePerformance12To24 { get; set; }

        [JsonProperty("Discrete24mTo36m_YE")]
        public string DiscretePerformance24To36 { get; set; }

        [JsonProperty("Discrete36mTo48m_YE")]
        public string DiscretePerformance36To48 { get; set; }

        [JsonProperty("BenchmarkDiscrete0mTo12m_YE")]
        public string Benchmark0DiscretePerformance0To12 { get; set; }

        [JsonProperty("BenchmarkDiscrete12mTo24m_YE")]
        public string Benchmark0DiscretePerformance12To24 { get; set; }

        [JsonProperty("BenchmarkDiscrete24mTo36m_YE")]
        public string Benchmark0DiscretePerformance24To36 { get; set; }

        [JsonProperty("BenchmarkDiscrete36mTo48m_YE")]
        public string Benchmark0DiscretePerformance36To48 { get; set; }

        [JsonProperty("BenchmarkP1Discrete0To12m_YE")]
        public string Benchmark1DiscretePerformance0To12 { get; set; }

        [JsonProperty("BenchmarkP1Discrete12mTo24m_YE")]
        public string Benchmark1DiscretePerformance12To24 { get; set; }

        [JsonProperty("BenchmarkP1Discrete24mTo36m_YE")]
        public string Benchmark1DiscretePerformance24To36 { get; set; }

        [JsonProperty("BenchmarkP1Discrete36mTo48m_YE")]
        public string Benchmark1DiscretePerformance36To48 { get; set; }

        [JsonProperty("BenchmarkP2Discrete0To12m_YE")]
        public string Benchmark2DiscretePerformance0To12 { get; set; }

        [JsonProperty("BenchmarkP2Discrete12mTo24m_YE")]
        public string Benchmark2DiscretePerformance12To24 { get; set; }

        [JsonProperty("BenchmarkP2Discrete24mTo36m_YE")]
        public string Benchmark2DiscretePerformance24To36 { get; set; }

        [JsonProperty("BenchmarkP2Discrete36mTo48m_YE")]
        public string Benchmark2DiscretePerformance36To48 { get; set; }

        [JsonProperty("SectorDiscrete0To12m_YE")]
        public string SectorDiscretePerformance0To12 { get; set; }

        [JsonProperty("SectorDiscrete12mTo24m_YE")]
        public string SectorDiscretePerformance12To24 { get; set; }

        [JsonProperty("SectorDiscrete24mTo36m_YE")]
        public string SectorDiscretePerformance24To36 { get; set; }

        [JsonProperty("SectorDiscrete36mTo48m_YE")]
        public string SectorDiscretePerformance36To48 { get; set; }

        [JsonProperty("Benchmarks")]
        public IEnumerable<Benchmark> Benchmarks { get; set; }

        [JsonProperty("SectorNameShort")]
        public string SectorName { get; set; }

        [JsonProperty("Cumulative1m_DE")]
        public string Cumulative1m { get; set; }

        [JsonProperty("CumulativeYTD_DE")]
        public string CumulativeYearToDate { get; set; }

        [JsonProperty("Cumulative3m_DE")]
        public string Cumulative3m { get; set; }

        [JsonProperty("Cumulative6m_DE")]
        public string Cumulative6m { get; set; }

        [JsonProperty("Cumulative1y_DE")]
        public string Cumulative1y { get; set; }

        [JsonProperty("Cumulative3y_DE")]
        public string Cumulative3y { get; set; }

        [JsonProperty("Cumulative5y_DE")]
        public string Cumulative5y { get; set; }

        [JsonProperty("CumulativeSinceLaunch_DE")]
        public string CumulativeSinceInception { get; set; }

        [JsonProperty("SectorCumulative1m_DE")]
        public string SectorCumulative1m { get; set; }

        [JsonProperty("SectorCumulativeYTD_DE")]
        public string SectorCumulativeYearToDate { get; set; }

        [JsonProperty("SectorCumulative3m_DE")]
        public string SectorCumulative3m { get; set; }

        [JsonProperty("SectorCumulative6m_DE")]
        public string SectorCumulative6m { get; set; }

        [JsonProperty("SectorCumulative1y_DE")]
        public string SectorCumulative1y { get; set; }

        [JsonProperty("SectorCumulative3y_DE")]
        public string SectorCumulative3y { get; set; }

        [JsonProperty("SectorCumulative5y_DE")]
        public string SectorCumulative5y { get; set; }

        [JsonProperty("SectorCumulativeSinceUnitLaunch_DE")]
        public string SectorCumulativeSinceUnitLaunch { get; set; }

        [JsonProperty("Cumulative1mQuart_DE")]
        public string Cumulative1mQuart { get; set; }

        [JsonProperty("CumulativeYTDQuart_DE")]
        public string CumulativeYearToDateQuart { get; set; }

        [JsonProperty("Cumulative3mQuart_DE")]
        public string Cumulative3mQuart { get; set; }

        [JsonProperty("Cumulative6mQuart_DE")]
        public string Cumulative6mQuart { get; set; }

        [JsonProperty("Cumulative1yQuart_DE")]
        public string Cumulative1yQuart { get; set; }

        [JsonProperty("Cumulative3yQuart_DE")]
        public string Cumulative3yQuart { get; set; }

        [JsonProperty("Cumulative5yQuart_DE")]
        public string Cumulative5yQuart { get; set; }
    }

    public class RegionBreakdown
    {
        [JsonProperty("RegionBDDate")]
        public string Date { get; set; }

        [JsonProperty("Breakdowns")]
        public Breakdowns Breakdowns { get; set; }
    }

    public class Benchmark
    {
        public string BenchmarkTypeName { get; set; }

        public string BenchmarkName { get; set; }
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

