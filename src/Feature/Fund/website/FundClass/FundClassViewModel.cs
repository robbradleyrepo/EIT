namespace LionTrust.Feature.Fund.FundClass
{
    using LionTrust.Feature.Fund.Models;

    public class FundClassViewModel
    {
        public IFundClassSelector FundSelector { get; set; }

        public string DefaultCitiCode { get; set; }
    }
}