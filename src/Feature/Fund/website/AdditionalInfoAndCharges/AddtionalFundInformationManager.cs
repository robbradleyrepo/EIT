namespace LionTrust.Feature.Fund.AdditionalInfoAndCharges
{
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Legacy.Models;
    using System.Linq;

    [Service(ServiceType = typeof(IAdditionalFundInformationManager), Lifetime = Lifetime.Singleton)]
    public class AddtionalFundInformationManager : IAdditionalFundInformationManager
    {
        private readonly IFundClassRepository _repository;

        public AddtionalFundInformationManager(IFundClassRepository repository)
        {
            this._repository = repository;
        }

        public AdditionalInfoAndChargesModel GetAdditionalInformation(IFundClass fundClass, string citiCode)
        {
            if (fundClass == null || string.IsNullOrEmpty(citiCode))
            {
                return null;
            }

            var data = _repository.GetData().FirstOrDefault(c => c.CitiCode == citiCode);
            return Map(fundClass, data);
        }

        private static AdditionalInfoAndChargesModel Map(IFundClass fundClass, FundDataResponseModel apiData)
        {
            var result = new AdditionalInfoAndChargesModel
            {
                OngoingCharges = string.IsNullOrEmpty(fundClass.OngoingCharges) ? apiData?.OngoingCharge : fundClass.OngoingCharges,
                SedolCode = string.IsNullOrEmpty(fundClass.SedolCode) ? apiData?.SedolCode : fundClass.SedolCode,
                ISINCode = string.IsNullOrEmpty(fundClass.ISINCode) ? apiData?.ISINCode : fundClass.ISINCode,
                InitialCharge = string.IsNullOrEmpty(fundClass.InitialCharge) ? apiData?.InitialCharge : fundClass.InitialCharge,

                /// TODO
                IncludedOFC = "TODO"
            };

            return result;
        }
    }
}