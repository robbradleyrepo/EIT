using System.IO;

namespace LionTrust.Feature.Fund.Api.Cache
{
    public interface IFundDataResponseModelPersistentCache
    {
        FundDataResponseModel[] GetData(string priceType = Constants.PriceTypes.One);
        void SetData(FundDataResponseModel[] data, string priceType = Constants.PriceTypes.One);
        FundDataResponseModel GetData(string citicode, string priceType = Constants.PriceTypes.One, string currency = "");
        void SetData(FundDataResponseModel data, string citicode, string priceType = Constants.PriceTypes.One, string currency = "");
    }
}
