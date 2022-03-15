namespace LionTrust.Feature.Fund.PerformanceTables
{
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Foundation.DI;
    using System.Linq;
    using System.Collections.Generic;
    using LionTrust.Foundation.Legacy.Models;

    [Service(ServiceType = typeof(ICumulativePerformanceManager), Lifetime = Lifetime.Singleton)]
    public class CumulativePerformanceManager : ICumulativePerformanceManager
    {
        private readonly IFundClassRepository _repository;

        public CumulativePerformanceManager(IFundClassRepository repository)
        {
            this._repository = repository;
        }

        public IEnumerable<PerformanceTableRow> GetPerformanceTableRows(string citiCode, IFundClass fundClassItem)
        {
            var fundClass = _repository.GetData().FirstOrDefault(d => d.CitiCode == citiCode);
            if (fundClass == null || fundClassItem == null)
            {
                _repository.SendEmailOnErrorForCiticode(citiCode);
                return new PerformanceTableRow[0];
            }

            var result = new List<PerformanceTableRow>();
            if (!string.IsNullOrEmpty(fundClass.FundName))
            {
                result.Add(new PerformanceTableRow(fundClass.FundName, 
                    new string[] 
                    { 
                        fundClass.Cumulative1m,                         
                        fundClass.Cumulative3m,
                        fundClass.Cumulative6m,
                        fundClass.CumulativeYearToDate,
                        fundClass.Cumulative1y,
                        fundClass.Cumulative3y,
                        fundClass.Cumulative5y,
                        fundClass.Cumulative10y,
                        fundClass.CumulativeSinceInception
                    }));
            }

            if (!string.IsNullOrEmpty(fundClass.SectorNameLong) && !fundClassItem.HideSectorRows)
            {
                result.Add(new PerformanceTableRow(fundClass.SectorNameLong, 
                    new string[]
                    {
                        fundClass.SectorCumulative1m,
                        fundClass.SectorCumulative3m,
                        fundClass.SectorCumulative6m,
                        fundClass.SectorCumulativeYearToDate,
                        fundClass.SectorCumulative1y,
                        fundClass.SectorCumulative3y,
                        fundClass.SectorCumulative5y,
                        fundClass.SectorCumulative10y,
                        fundClass.SectorCumulativeSinceInception
                    }));
            }

            if (fundClass.Benchmarks == null)
            {
                return result;
            }

            var benchmark0 = fundClass.Benchmarks.FirstOrDefault(b => b.BenchmarkTypeName == "Benchmark");
            if (benchmark0 != null && !fundClassItem.HideBenchmarkRows)
            {
                result.Add(new PerformanceTableRow(benchmark0.BenchmarkName, new string[]
                {
                    fundClass.Benchmark0Cumulative1m,
                    fundClass.Benchmark0Cumulative3m,
                    fundClass.Benchmark0Cumulative6m,
                    fundClass.Benchmark0CumulativeYearToDate,
                    fundClass.Benchmark0Cumulative1y,
                    fundClass.Benchmark0Cumulative3y,
                    fundClass.Benchmark0Cumulative5y,
                    fundClass.Benchmark0Cumulative10y,
                    fundClass.Benchmark0CumulativeSinceInception
                }));
            }

            var benchmark1 = fundClass.Benchmarks.FirstOrDefault(b => b.BenchmarkTypeName == "Benchmark Comparator 1");
            if (benchmark1 != null && !fundClassItem.HideBenchmarkComparator1Rows)
            {
                result.Add(new PerformanceTableRow(benchmark1.BenchmarkName, new string[]
                {
                    fundClass.Benchmark1Cumulative1m,
                    fundClass.Benchmark1Cumulative3m,
                    fundClass.Benchmark1Cumulative6m,
                    fundClass.Benchmark1CumulativeYearToDate,
                    fundClass.Benchmark1Cumulative1y,
                    fundClass.Benchmark1Cumulative3y,
                    fundClass.Benchmark1Cumulative5y,
                    fundClass.Benchmark1Cumulative10y,
                    fundClass.Benchmark1CumulativeSinceInception
                }));
            }

            var benchmark2 = fundClass.Benchmarks.FirstOrDefault(b => b.BenchmarkTypeName == "Benchmark Comparator 2");
            if (benchmark2 != null && !fundClassItem.HideBenchmarkComparator2Rows)
            {
                result.Add(new PerformanceTableRow(benchmark2.BenchmarkName, new string[]
                {
                    fundClass.Benchmark2Cumulative1m,
                    fundClass.Benchmark2Cumulative3m,
                    fundClass.Benchmark2Cumulative6m,
                    fundClass.Benchmark2CumulativeYearToDate,
                    fundClass.Benchmark2Cumulative1y,
                    fundClass.Benchmark2Cumulative3y,
                    fundClass.Benchmark2Cumulative5y,
                    fundClass.Benchmark2Cumulative10y,
                    fundClass.Benchmark2CumulativeSinceInception
                }));
            }            

            return result;
        }               

        public PerformanceTableRow GetQuartile(string citiCode, IFundClass fundClassItem)
        {
            var fundClass = _repository.GetData().FirstOrDefault(d => d.CitiCode == citiCode);
            if (fundClass == null || fundClassItem == null)
            {
                _repository.SendEmailOnErrorForCiticode(citiCode);
                return null;
            }

            if (fundClassItem.HideQuartileRows)
            {
                return null;
            }

            return new PerformanceTableRow(string.Empty, new string[] 
            { 
                fundClass.Cumulative1mQuart,                 
                fundClass.Cumulative3mQuart, 
                fundClass.Cumulative6mQuart,
                fundClass.CumulativeYearToDateQuart,
                fundClass.Cumulative1yQuart,
                fundClass.Cumulative3yQuart,
                fundClass.Cumulative5yQuart,
                fundClass.Cumulative10yQuart,
                string.Empty
            });
        }        
    }
}