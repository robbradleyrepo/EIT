namespace LionTrust.Feature.Fund.AdditionalInfoAndCharges
{
    using System.Linq;
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Foundation.Legacy.Models;

    public class AdditionalInfoAndChargesDetails
    {
        private readonly IFundClassRepository _repository;

        public AdditionalInfoAndChargesDetails(IFundClassRepository repository)
        {
            this._repository = repository;
        }

        public AdditionalInfoAndChargesModel GetDetails(IFundClass fundClass, string citiCode)
        {
            var result = Map(fundClass);

            if (string.IsNullOrEmpty(citiCode))
            {
                return result;
            }

            var apiDetails = _repository.GetData().FirstOrDefault(f => f.CitiCode == citiCode);
            if (apiDetails == null)
            {
                _repository.SendEmailOnErrorForCiticode(citiCode);
                return result;
            }

            return Consolidate(result, apiDetails);
        }

        private static AdditionalInfoAndChargesModel Consolidate(AdditionalInfoAndChargesModel model, FundDataResponseModel apiData)
        {
            if (string.IsNullOrEmpty(model.AnnualManagementCharge))
            {
                model.AnnualManagementCharge = apiData.AnnualManagementCharge;
            }

            if (string.IsNullOrEmpty(model.InitialCharge))
            {
                model.InitialCharge = apiData.InitialCharge;
            }

            if (string.IsNullOrEmpty(model.ISINCode))
            {
                model.ISINCode = apiData.ISINCode;
            }

            if (string.IsNullOrEmpty(model.OngoingCharges))
            {
                model.OngoingCharges = apiData.OngoingCharge;
            }

            if (string.IsNullOrEmpty(model.SedolCode))
            {
                model.SedolCode = apiData.SedolCode;
            }

            return model;
        }

        private static AdditionalInfoAndChargesModel Map(IFundClass fundClass)
        {
            return new AdditionalInfoAndChargesModel
            {                
                AnnualManagementCharge = fundClass.AnnualManagemnetCharge,
                InitialCharge = fundClass.InitialCharge,
                ISINCode = fundClass.ISINCode,
                OngoingCharges = fundClass.OngoingCharges,
                SedolCode = fundClass.SedolCode
            };
        }
    }
}