namespace LionTrust.Feature.Fund.Api
{
    using LionTrust.Foundation.DI;
    using Newtonsoft.Json;
    using RestSharp;
    using Sitecore.Abstractions;
    using System.Linq;

    [Service(ServiceType = typeof(IFundClassRepository), Lifetime = Lifetime.Singleton)]
    public class ExtrnalFundClassRepository : IFundClassRepository
    {
        private readonly BaseSettings _settings;

        private FundDataResponseModel[] _data;

        private object locker = new object();

        public ExtrnalFundClassRepository(BaseSettings settings)
        {
            this._settings = settings;
        }

        private void LoadData()
        {
            var url = _settings.GetSetting("LionTrust.Feature.Fund.FeApiEndPoint");
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            var client = new RestClient(url);
            var request = new RestRequest("/api/funddata/liontrust/C9E1ACC5-B0E7-400A-A5BF-83C68654EA0B");
            request.AddQueryParameter("rangename", "LiontrustFunds");
            var result = client.Execute(request);
            if (!result.IsSuccessful)
            {
                return;
            }

            var fundDetails = JsonConvert.DeserializeObject<FundDataResponseModel[]>(result.Content);
            if (fundDetails == null || !fundDetails.Any())
            {
                return;
            }

            _data = fundDetails;
        }

        public FundDataResponseModel[] GetData()
        {
            lock (locker)
            {
                if (_data == null)
                {
                    LoadData();
                }

                return _data;
            }
        }
    }
}