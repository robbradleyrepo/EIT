namespace LionTrust.Feature.Fund.Models
{
    using LionTrust.Foundation.Legacy.Models;

    public class KeyInfoPriceViewModel
    {     
        public IKeyInfoPriceComponent Component { get; set; }

        public IFund Fund { get; set; }
    }
}