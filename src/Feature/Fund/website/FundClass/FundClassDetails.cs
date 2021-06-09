namespace LionTrust.Feature.Fund.FundClass
{
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Legacy.Models;
    using System;
    using System.Linq;

    [Service(ServiceType = typeof(IFundClassDetails), Lifetime = Lifetime.Singleton)]
    public class FundClassDetails : IFundClassDetails
    {
        private readonly IFundClassRepository _repository;

        public FundClassDetails(IFundClassRepository repository)
        {
            this._repository = repository;
        }

        public FundClassData GetFundClassDetails(IFundClass fundClass)
        {
            var apiData = _repository.GetData().FirstOrDefault(f => f.CitiCode == fundClass.CitiCode);
            var result = Map(fundClass);
            if (apiData == null)
            {
                return result;
            }

            return ConsolidateData(result, apiData);
        }

        private static FundClassData ConsolidateData(FundClassData data, FundDataResponseModel apiData)
        {
            if (data.ClassLaunchDate == null || data.ClassLaunchDate == DateTime.MinValue)
            {
                if (DateTime.TryParse(apiData.UnitLaunchDate, out DateTime date))
                {
                    data.ClassLaunchDate = date;
                }
            }

            /// TBD
            if (string.IsNullOrEmpty(data.Comparator))
            {
                
            }
            /// TBD
            if (string.IsNullOrEmpty(data.Duration))
            {

            }

            if (string.IsNullOrEmpty(data.OfferPrice))
            {
                data.OfferPrice = apiData.OfferPrice;
            }

            if (data.PriceDate == null || data.PriceDate == DateTime.MinValue)
            {
                if (DateTime.TryParse(apiData.UnitPriceDate, out DateTime priceDate))
                {
                    data.PriceDate = priceDate;
                }
            }

            if (string.IsNullOrEmpty(data.SinglePrice))
            {
                data.SinglePrice = apiData.SinglePrice;
            }

            return data;
        }

        private static FundClassData Map(IFundClass fundClass)
        {
            return new FundClassData
            {
                ClassLaunchDate = fundClass.ClassLaunchDate,
                Comparator = fundClass.Comparator,
                Duration = fundClass.Duration,
                OfferPrice = fundClass.OfferPrice,
                PriceDate = fundClass.PriceDate,
                SinglePrice = fundClass.SinglePrice
            };
        }
    }
}