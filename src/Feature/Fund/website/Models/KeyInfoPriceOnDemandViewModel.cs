using LionTrust.Feature.Fund.FundClass;

namespace LionTrust.Feature.Fund.Models
{
    public class KeyInfoPriceOnDemandViewModel
    {     
        public IKeyInfoPriceOnDemandComponent Component { get; set; }
        public KeyInfoDataOnDemand FundValues { get; set; }
    }
}