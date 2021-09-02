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

            if (string.IsNullOrEmpty(data.Comparator1))
            {
                data.Comparator1 = apiData?.Benchmarks?
                    .FirstOrDefault(x => x.BenchmarkTypeName.ToLower().Contains("benchmark comparator 1"))?
                    .BenchmarkName;
            }

            if (string.IsNullOrEmpty(data.Comparator2))
            {
                data.Comparator2 = apiData?.Benchmarks?
                    .FirstOrDefault(x => x.BenchmarkTypeName.ToLower().Contains("benchmark comparator 2"))?
                    .BenchmarkName;
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
                Comparator1 = fundClass.Comparator1,
                Comparator2 = fundClass.Comparator2,
                OfferPrice = fundClass.OfferPrice,
                PriceDate = fundClass.PriceDate,
                SinglePrice = fundClass.SinglePrice
            };
        }
    }
}