namespace LionTrust.Feature.Fund.Models
{
    using LionTrust.Feature.Fund.FundClass;
    using LionTrust.Foundation.Legacy.Models;

    public class KeyInfoPriceViewModel
    {     
        public IKeyInfoPriceComponent Component { get; set; }

        public IFund Fund { get; set; }        

        public FundClassData ClassData { get; set; }

    }
}