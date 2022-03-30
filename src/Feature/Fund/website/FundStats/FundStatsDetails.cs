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

            if (apiData == null)
            {
                _repository.SendEmailOnErrorForCiticode(fundClass.CitiCode);
                return null;
            }

            if (int.TryParse(apiData.NumberOfHoldings, out int nrOfHoldings))
            {
                nrOfHoldings -= 1;
            }

            return new FundStatsData
            {
                Holdings = Convert.ToString(nrOfHoldings),
                FundSize = apiData.FundSize
            };
        }

        public FundStatsDataOnDemand GetFundStatsDetailsOnDemand(IFundClass fundClass, string priceType)
        {
            var apiData = _repository.GetDataOnDemand(fundClass.CitiCode, priceType);

            if (apiData == null)
            {
                _repository.SendEmailOnErrorForCiticode(fundClass.CitiCode);
                return null;
            }

            return new FundStatsDataOnDemand
            {
                SharePrice = apiData.SinglePrice
            };
        }
    }
}