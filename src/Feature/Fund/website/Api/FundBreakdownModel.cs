namespace LionTrust.Feature.Fund.Api
{
    public class FundBreakdownModel
    {
        public string Name { get; set; }

        public string Weight { get; set; }

        public string WeightWithoutPercentage 
        { 
            get
            {
                if (string.IsNullOrEmpty(Weight))
                {
                    return string.Empty;
                }

                return Weight.Replace("%", string.Empty);
            }
        }
    }
}