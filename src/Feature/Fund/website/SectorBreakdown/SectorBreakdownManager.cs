namespace LionTrust.Feature.Fund.SectorBreakdown
{
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Foundation.DI;
    using System.Linq;
    using System.Collections.Generic;

    [Service(ServiceType = typeof(ISectorBreakdownManager), Lifetime = Lifetime.Singleton)]
    public class SectorBreakdownManager : ISectorBreakdownManager
    {
        private readonly IFundClassRepository _repository;

        public SectorBreakdownManager(IFundClassRepository repository)
        {
            this._repository = repository;
        }

        public IEnumerable<FundBreakdownModel> GetFundClassBreakdowns(string citiCode)
        {
            var apiData = _repository.GetData().FirstOrDefault(f => f.CitiCode == citiCode);
            if (apiData == null)
            {
                _repository.SendEmailOnErrorForCiticode(citiCode);
                return new FundBreakdownModel[0];

            }

            if (apiData.SectorBreakdown == null)
            {
                return new FundBreakdownModel[0];
            }

            return apiData.SectorBreakdown.Breakdowns.Data.Select(bd => new FundBreakdownModel { Name = bd.Name, Weight = bd.Weight });
        }
    }
}