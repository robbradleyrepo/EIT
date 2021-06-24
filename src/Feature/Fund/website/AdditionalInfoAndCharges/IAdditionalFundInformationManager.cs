namespace LionTrust.Feature.Fund.AdditionalInfoAndCharges
{
    using LionTrust.Foundation.Legacy.Models;

    public interface IAdditionalFundInformationManager
    {
        AdditionalInfoAndChargesModel GetAdditionalInformation(IFundClass component, string citiCode);
    }
}
