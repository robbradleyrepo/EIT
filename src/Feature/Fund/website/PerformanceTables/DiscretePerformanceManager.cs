namespace LionTrust.Feature.Fund.PerformanceTables
{
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Foundation.DI;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using System.Globalization;

    [Service(ServiceType = typeof(IDiscretePerformanceManager), Lifetime = Lifetime.Singleton)]
    public class DiscretePerformanceManager : IDiscretePerformanceManager
    {
        private readonly IFundClassRepository _repository;

        public DiscretePerformanceManager(IFundClassRepository repository)
        {
            this._repository = repository;
        }

        public IEnumerable<PerformanceTableRow> GetPerformanceTableRows(string citiCode)
        {
            var fundClass = _repository.GetData().FirstOrDefault(d => d.CitiCode == citiCode);
            if (fundClass == null)
            {
                return new PerformanceTableRow[0];
            }

            var result = new List<PerformanceTableRow>();
            if (!string.IsNullOrEmpty(fundClass.FundName))
            {
                result.Add(new PerformanceTableRow(fundClass.FundName,
                    new string[]
                    {
                        fundClass.DiscretePerformance0To12,
                        fundClass.DiscretePerformance12To24,
                        fundClass.DiscretePerformance24To36,
                        fundClass.DiscretePerformance36To48
                    }));
            }

            if (!string.IsNullOrEmpty(fundClass.SectorName))
            {
                result.Add(new PerformanceTableRow(fundClass.SectorName, new string[]
                {
                    fundClass.SectorDiscretePerformance0To12,
                    fundClass.SectorDiscretePerformance12To24,
                    fundClass.SectorDiscretePerformance24To36,
                    fundClass.SectorDiscretePerformance36To48
                }));
            }

            if (fundClass.Benchmarks == null)
            {
                return new PerformanceTableRow[0];
            }

            var benchmark0 = fundClass.Benchmarks.FirstOrDefault(b => b.BenchmarkTypeName == "Benchmark");
            if (benchmark0 != null)
            {
                result.Add(new PerformanceTableRow(benchmark0.BenchmarkName, new string[]
                {
                    fundClass.Benchmark0DiscretePerformance0To12,
                    fundClass.Benchmark0DiscretePerformance12To24,
                    fundClass.Benchmark0DiscretePerformance24To36,
                    fundClass.Benchmark0DiscretePerformance36To48
                }));
            }

            var benchmark1 = fundClass.Benchmarks.FirstOrDefault(b => b.BenchmarkTypeName == "Benchmark Comparator 1");
            if (benchmark1 != null)
            {
                result.Add(new PerformanceTableRow(benchmark1.BenchmarkName, new string[]
                {
                    fundClass.Benchmark1DiscretePerformance0To12,
                    fundClass.Benchmark1DiscretePerformance12To24,
                    fundClass.Benchmark1DiscretePerformance24To36,
                    fundClass.Benchmark1DiscretePerformance36To48
                }));
            }

            var benchmark2 = fundClass.Benchmarks.FirstOrDefault(b => b.BenchmarkTypeName == "Benchmark Comparator 2");
            if (benchmark2 != null)
            {
                result.Add(new PerformanceTableRow(benchmark2.BenchmarkName, new string[]
                {
                    fundClass.Benchmark2DiscretePerformance0To12,
                    fundClass.Benchmark2DiscretePerformance12To24,
                    fundClass.Benchmark2DiscretePerformance24To36,
                    fundClass.Benchmark2DiscretePerformance36To48
                }));
            }            

            return result.Where(x => x.Columns.Any(v => v.HasValue));
        }

        public PerformanceTableRow GetQuartile(string citiCode)
        {
            var fundClass = _repository.GetData().FirstOrDefault(d => d.CitiCode == citiCode);
            if (fundClass == null)
            {
                return null;
            }

            return new PerformanceTableRow(string.Empty, new string[]
            {
                fundClass.DiscreteQuartile0To12m,
                fundClass.DiscreteQuartile12mTo24m,
                fundClass.DiscreteQuartile24mTo36m,
                fundClass.DiscreteQuartile36mTo48m
            });
        }

        public string[] GetColumnHeadings(string citiCode)
        {
            var result = new List<string>();
            for (int i = -1; i >= -4; i--)
            {
                var qeMonth = GetPerformanceQEMonth(citiCode);
                result.Add($"{qeMonth} {DateTime.Now.AddYears(i).ToString("yy")}");
            }

            return result.ToArray();
        }

        private string GetPerformanceQEMonth(string citiCode)
        {
            var fundClass = _repository.GetData().FirstOrDefault(d => d.CitiCode == citiCode);
            if (fundClass == null)
            {
                return null;
            }

            var qeMonth = string.Empty;

            if (!string.IsNullOrEmpty(fundClass.SectorDiscretePerformanceQE))
            {
                DateTime.TryParseExact(fundClass.SectorDiscretePerformanceQE, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime qeDate);
                if (qeDate != null)
                {
                    qeMonth = qeDate.ToString("MMM");
                }
            }
                
            return qeMonth;
        }
    }
}