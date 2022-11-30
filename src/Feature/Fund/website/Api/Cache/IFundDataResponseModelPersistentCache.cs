using System.IO;

namespace LionTrust.Feature.Fund.Api.Cache
{
    public interface IFundDataResponseModelPersistentCache
    {
        FundDataResponseModel[] GetData(string priceType = "1");
        void SetData(FundDataResponseModel[] data, string priceType = "1");
        FundDataResponseModel GetData(string citicode, string priceType = "1", string currency = "");
        void SetData(FundDataResponseModel data, string citicode, string priceType = "1", string currency = "");
    }
}
