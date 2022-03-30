namespace LionTrust.Feature.Fund.Models
{
    using LionTrust.Feature.Fund.AdditionalInfoAndCharges;

    public class AdditionalInfoOnDemandViewModel
    {
        public AdditionalInfoAndChargesModel Data { get; set; }

        public IAdditionalInfoOnDemandComponent Component { get; set; }
    }
}