using LionTrust.Foundation.Legacy.Models;

namespace LionTrust.Feature.Fund.Models
{
    public class FundDisclaimerViewModel
    {
        public IFundDisclaimer Component { get; set; }
        public IFund Fund { get; set; }
    }
}