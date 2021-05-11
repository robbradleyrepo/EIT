namespace LionTrust.Feature.Fund.Models
{
    using LionTrust.Foundation.Legacy.Models;

    public class KeyInfoPriceViewModel
    {
        public IFundExtended KeyInfoPrice { get; set; }
        public IKeyInfoPriceComponent Component { get; set; }
    }
}