namespace LionTrust.Feature.Fund.Api
{
    public interface IFundClassRepository
    {
        FundDataResponseModel[] GetData();
        void UpdateData();
        void SendEmailOnError(string errorMessage);
        void SendEmailOnErrorForCiticode(string citicode);
    }
}
