namespace LionTrust.Feature.Fund.Api
{
    using LionTrust.Feature.Fund.Repository;

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

                return FundRepository.GetTwoDecimalsFormat(Weight.Replace("%", string.Empty));
            }
        }        
    }
}