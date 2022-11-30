namespace LionTrust.Feature.Fund.PerformanceTables
{
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Foundation.DI;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using System.Globalization;
    using LionTrust.Foundation.Legacy.Models;

    [Service(ServiceType = typeof(IDiscretePerformanceManager), Lifetime = Lifetime.Singleton)]
    public class DiscretePerformanceManager : IDiscretePerformanceManager
    {
        private readonly IFundClassRepository _repository;

        public DiscretePerformanceManager(IFundClassRepository repository)
        {
            this._repository = repository;
        }

        public IEnumerable<PerformanceTableRow> GetPerformanceTableRows(string citiCode, IFundClass fundClassItem)
        {
            var fundClass = _repository.GetDataOnDemand(citiCode, Constants.PriceTypes.One, fundClassItem.Currency);
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
                        fundClass.DiscretePerformance0To12,
                        fundClass.DiscretePerformance12To24,
                        fundClass.DiscretePerformance24To36,
                        fundClass.DiscretePerformance36To48,
                        fundClass.DiscretePerformance48To60
                    }));
            }

            if (!string.IsNullOrEmpty(fundClass.SectorNameLong) && !fundClassItem.HideSectorRows)
            {
                result.Add(new PerformanceTableRow(fundClass.SectorNameLong,
                    new string[]
                    {
                        fundClass.SectorDiscretePerformance0To12,
                        fundClass.SectorDiscretePerformance12To24,
                        fundClass.SectorDiscretePerformance24To36,
                        fundClass.SectorDiscretePerformance36To48,
                        fundClass.SectorDiscretePerformance48To60
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
                    fundClass.Benchmark0DiscretePerformance0To12,
                    fundClass.Benchmark0DiscretePerformance12To24,
                    fundClass.Benchmark0DiscretePerformance24To36,
                    fundClass.Benchmark0DiscretePerformance36To48,
                    fundClass.Benchmark0DiscretePerformance48To60
                }));
            }

            var benchmark1 = fundClass.Benchmarks.FirstOrDefault(b => b.BenchmarkTypeName == "Benchmark Comparator 1");
            if (benchmark1 != null && !fundClassItem.HideBenchmarkComparator1Rows)
            {
                result.Add(new PerformanceTableRow(benchmark1.BenchmarkName, new string[]
                {
                    fundClass.Benchmark1DiscretePerformance0To12,
                    fundClass.Benchmark1DiscretePerformance12To24,
                    fundClass.Benchmark1DiscretePerformance24To36,
                    fundClass.Benchmark1DiscretePerformance36To48,
                    fundClass.Benchmark1DiscretePerformance48To60
                }));
            }

            var benchmark2 = fundClass.Benchmarks.FirstOrDefault(b => b.BenchmarkTypeName == "Benchmark Comparator 2");
            if (benchmark2 != null && !fundClassItem.HideBenchmarkComparator2Rows)
            {
                result.Add(new PerformanceTableRow(benchmark2.BenchmarkName, new string[]
                {
                    fundClass.Benchmark2DiscretePerformance0To12,
                    fundClass.Benchmark2DiscretePerformance12To24,
                    fundClass.Benchmark2DiscretePerformance24To36,
                    fundClass.Benchmark2DiscretePerformance36To48,
                    fundClass.Benchmark2DiscretePerformance48To60
                }));
            }            

            return result.Where(x => x.Columns.Any(v => v.HasValue));
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
                fundClass.DiscreteQuartile0To12m,
                fundClass.DiscreteQuartile12mTo24m,
                fundClass.DiscreteQuartile24mTo36m,
                fundClass.DiscreteQuartile36mTo48m,
                fundClass.DiscreteQuartile48mTo60m
            });
        }

        public string[] GetColumnHeadings(string citiCode)
        {
            var result = new List<string>();
            var qeDate = GetPerformanceQEDate(citiCode);
            var qeMonth = GetPerformanceQEMonth(qeDate);
            var startIndex = qeDate.Year < DateTime.Now.Year ? -1 : 0;
            var endIndex = startIndex - 4;
            for (int i = startIndex; i >= endIndex; i--)
            {                
                result.Add($"{qeMonth} {DateTime.Now.AddYears(i).ToString("yy")}");
            }

            return result.ToArray();
        }

        private string GetPerformanceQEMonth(DateTime qeDate)
        {
            var qeMonth = string.Empty;
            if (qeDate != null && qeDate != DateTime.MinValue)
            {
                qeMonth = qeDate.ToString("MMM");
            }
                
            return qeMonth;
        }

        private DateTime GetPerformanceQEDate(string citiCode)
        {
            var fundClass = _repository.GetData().FirstOrDefault(d => d.CitiCode == citiCode);
            if (fundClass == null)
            {
                _repository.SendEmailOnErrorForCiticode(citiCode);
                return DateTime.MinValue;
            }

            if (!string.IsNullOrEmpty(fundClass.DiscretePerformanceQE))
            {
                DateTime.TryParseExact(fundClass.DiscretePerformanceQE, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime qeDate);
                if (qeDate != null)
                {
                    return qeDate;
                }
            }

            return DateTime.MinValue;
        }
    }
}