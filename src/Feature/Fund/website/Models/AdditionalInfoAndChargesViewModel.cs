namespace LionTrust.Feature.Fund.Models
{
    using LionTrust.Foundation.Legacy.Models;

    public class AdditionalInfoAndChargesViewModel
    {
        public IFundExtended AdditionalInfoAndCharges { get; set; }
        public IAdditionalInfoAndChargesComponent Component { get; set; }
    }
}