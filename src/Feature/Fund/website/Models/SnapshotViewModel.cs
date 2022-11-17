using LionTrust.Feature.Fund.FundClass;

namespace LionTrust.Feature.Fund.Models
{
    public class SnapshotViewModel
    {     
        public ISnapshot Component { get; set; }
        public KeyInfoDataOnDemand FundValues { get; set; }
    }
}