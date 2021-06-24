namespace LionTrust.Feature.Fund.Models
{
    using LionTrust.Feature.Fund.AdditionalInfoAndCharges;
    using LionTrust.Foundation.Legacy.Models;

    public class AdditionalInfoAndChargesViewModel
    {
        public IFundClass FundClass { get; set; }

        public AdditionalInfoAndChargesModel Data { get; set; }

        public IAdditionalInfoAndChargesComponent Component { get; set; }
    }
}