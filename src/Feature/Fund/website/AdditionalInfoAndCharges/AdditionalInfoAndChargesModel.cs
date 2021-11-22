using LionTrust.Feature.Fund.Repository;

namespace LionTrust.Feature.Fund.AdditionalInfoAndCharges
{
    public class AdditionalInfoAndChargesModel
    {                
        public string AnnualManagementCharge { get; set; }

        public string InitialCharge { get; set; }

        public string ISINCode { get; set; }

        public string OngoingCharges { get; set; }

        public string SedolCode { get; set; }

        public string FormattedInitialCharge
        {
            get
            {
                if (string.IsNullOrEmpty(InitialCharge))
                {
                    return string.Empty;
                }

                return FundRepository.GetPercentageTwoDecimalsFormat(InitialCharge);
            }
        }

        public string FormattedOngoingCharges
        {
            get
            {
                if (string.IsNullOrEmpty(OngoingCharges))
                {
                    return string.Empty;
                }

                return FundRepository.GetPercentageTwoDecimalsFormat(OngoingCharges);
            }
        }

        public string FormattedAnnualManagementCharge
        {
            get
            {
                if (string.IsNullOrEmpty(AnnualManagementCharge))
                {
                    return string.Empty;
                }

                return FundRepository.GetPercentageTwoDecimalsFormat(AnnualManagementCharge);
            }
        }
    }
}