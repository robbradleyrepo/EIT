namespace LionTrust.Feature.Fund.Models
{
    using LionTrust.Feature.Fund.AdditionalInfoAndCharges;
    using LionTrust.Foundation.Legacy.Models;

    public class AdditionalInfoAndChargesViewModel
    {
        public IFund Fund { get; set; }

        public AdditionalInfoAndChargesModel Data { get; set; }

        public IAdditionalInfoAndChargesComponent Component { get; set; }
    }
}