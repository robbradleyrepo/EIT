namespace LionTrust.Feature.Fund.FundPerformance
{
    public class FundPerformanceGraphViewModel
    {
        public IFundPerformanceGraph Component { get; set; }

        public string CitiCode { get; set; }

        public string FactsheetUrl { get; set; }

        public bool Hide { get; set; }

        public string GraphUrl()
        {
            return $"https://digital-tools.feprecisionplus.com/liontrustchart/charting/en-gb/liontrust?citicode={CitiCode}";
        }        
    }
}