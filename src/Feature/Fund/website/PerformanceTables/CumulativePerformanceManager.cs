namespace LionTrust.Feature.Fund.PerformanceTables
{
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Foundation.DI;
    using System.Linq;
    using System.Collections.Generic;

    [Service(ServiceType = typeof(ICumulativePerformanceManager), Lifetime = Lifetime.Singleton)]
    public class CumulativePerformanceManager : ICumulativePerformanceManager
    {
        private readonly IFundClassRepository _repository;

        public CumulativePerformanceManager(IFundClassRepository repository)
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
                        fundClass.Cumulative1m,                         
                        fundClass.Cumulative3m,
                        fundClass.Cumulative6m,
                        fundClass.CumulativeYearToDate,
                        fundClass.Cumulative1y,
                        fundClass.Cumulative3y,
                        fundClass.Cumulative5y,
                        fundClass.CumulativeSinceInception,
                    }));
            }
            
            if (!string.IsNullOrEmpty(fundClass.SectorName))
            {
                result.Add(new PerformanceTableRow(fundClass.SectorName, new string[]
                {
                    fundClass.SectorCumulative1m,                    
                    fundClass.SectorCumulative3m,
                    fundClass.SectorCumulative6m,
                    fundClass.SectorCumulativeYearToDate,
                    fundClass.SectorCumulative1y,
                    fundClass.SectorCumulative3y,
                    fundClass.SectorCumulative5y,
                    fundClass.SectorCumulativeSinceUnitLaunch,
                }));
            }

            return result;
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
                fundClass.Cumulative1mQuart,                 
                fundClass.Cumulative3mQuart, 
                fundClass.Cumulative6mQuart,
                fundClass.CumulativeYearToDateQuart,
                fundClass.Cumulative1yQuart,
                fundClass.Cumulative3yQuart,
                fundClass.Cumulative5yQuart
            });
        }        
    }
}