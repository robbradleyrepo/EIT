namespace LionTrust.Feature.Fund.Api
{
    using LionTrust.Foundation.DI;
    using Newtonsoft.Json;
    using RestSharp;
    using Sitecore.Abstractions;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(IFundHoldingsBreakdown), Lifetime = Lifetime.Singleton)]
    public class BasicFundHoldingsBreakdown : IFundHoldingsBreakdown
    {
        private readonly BaseSettings _settings;

        public BasicFundHoldingsBreakdown(BaseSettings settings)
        {
            this._settings = settings;
        }

        public IEnumerable<FundBreakdownModel> GetFundHoldings(string citiCode)
        {
            var url = _settings.GetSetting("LionTrust.Feature.Fund.FeApiEndPoint");
            if (string.IsNullOrEmpty(url))
            {
                return new FundBreakdownModel[0];
            }

            var client = new RestClient(url);
            var request = new RestRequest("/api/funddata/liontrust/C9E1ACC5-B0E7-400A-A5BF-83C68654EA0B");
            request.AddQueryParameter("rangename", "LiontrustFunds");
            request.AddQueryParameter("Citicodes", citiCode);
            var result = client.Execute(request);
            if (!result.IsSuccessful)
            {
                return new FundBreakdownModel[0];
            }

            var fundDetails = JsonConvert.DeserializeObject<FundDataResponseModel[]>(result.Content);
            if (fundDetails == null || !fundDetails.Any())
            {
                return new FundBreakdownModel[0];
            }
            return fundDetails.First().Holdings.Breakdowns.Data.Select(b => new FundBreakdownModel { Name = b.Name, Weight = b.Weight });
        }
    }
}