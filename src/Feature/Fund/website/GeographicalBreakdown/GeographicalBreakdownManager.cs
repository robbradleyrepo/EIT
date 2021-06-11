namespace LionTrust.Feature.Fund.GeographicalBreakdown
{
    using LionTrust.Feature.Fund.Api;
    using LionTrust.Foundation.DI;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(IGeographicalBreakdownManager), Lifetime = Lifetime.Singleton)]
    public class GeographicalBreakdownManager: IGeographicalBreakdownManager
    {
        private readonly IFundClassRepository _repository;

        public GeographicalBreakdownManager(IFundClassRepository repository)
        {
            this._repository = repository;
        }


        public IEnumerable<FundBreakdownModel> GetBreakdowns(string citiCode)
        {
            var apiData = _repository.GetData().FirstOrDefault(f => f.CitiCode == citiCode);
            if (apiData == null || apiData.SectorBreakdown == null)
            {
                return new FundBreakdownModel[0];
            }

            return apiData.RegionBreakdown.Breakdowns.Data.Select(bd => new FundBreakdownModel { Name = bd.Name, Weight = bd.Weight });
        }
    }
}
