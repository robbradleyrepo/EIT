namespace LionTrust.Feature.Fund.Api
{
    using System.Text.RegularExpressions;

    public class FundBreakdownModel
    {
        public string Name { get; set; }

        public string Weight { get; set; }

        public string FormattedWeight
        {
            get
            {
                if (string.IsNullOrEmpty(Weight))
                {
                    return string.Empty;
                }

                var match = new Regex(@".*\.[0-9]");
                var result = match.Match(Weight);
                if (result.Success)
                {
                    return result.Value;
                }

                return Weight;
            }
        }

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