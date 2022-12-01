namespace LionTrust.Feature.Fund.Api
{
    public interface IFundClassRepository
    {
        FundDataResponseModel[] GetData();
        void UpdateData(bool force = false);
        void SendEmailOnErrorForCiticode(string citicode);
        FundDataResponseModel GetDataOnDemand(string citicode, string priceType = Constants.PriceTypes.One, string currency = "");
    }
}
