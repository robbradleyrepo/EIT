namespace LionTrust.Feature.Fund.FundStats
{
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Legacy.Models;
    using System;
    using System.Linq;

    [Service(ServiceType = typeof(IFundStatsDetails), Lifetime = Lifetime.Singleton)]
    public class FundStatsDetails : IFundStatsDetails
    {
        private readonly IFundClassRepository _repository;

        public FundStatsDetails(IFundClassRepository repository)
        {
            this._repository = repository;
        }

        public FundStatsData GetFundStatsDetails(IFundClass fundClass)
        {
            var apiData = _repository.GetData().FirstOrDefault(f => f.CitiCode == fundClass.CitiCode);
            if (apiData != null)
            {
                return new FundStatsData
                {
                    Holdings = apiData.NumberOfHoldings,
                    FundSize = apiData.FundSize,
                    FundDate = apiData.UnitLaunchDate
                };
            }
            return null;
        }
    }
}