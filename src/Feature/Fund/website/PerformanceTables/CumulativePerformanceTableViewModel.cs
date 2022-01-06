namespace LionTrust.Feature.Fund.PerformanceTables
{
    public class CumulativePerformanceTableViewModel: PerformanceTableViewModel
    {
        public override string[] ColumnHeadings
        {
            get
            {
                return new string[]
                {
                   "1 month", "3 months", "6 months", "YTD", "1 year", "3 years", "5 years", "Since Inception"
                };
            }
        }
    }
}