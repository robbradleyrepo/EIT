namespace LionTrust.Feature.Fund.Api
{
    using LionTrust.Feature.Fund.Api.Cache;
    using LionTrust.Feature.Fund.Api.Error;
    using LionTrust.Foundation.DI;
    using Newtonsoft.Json;
    using RestSharp;
    using Sitecore.Abstractions;
    using Sitecore.Diagnostics;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(IFundClassRepository), Lifetime = Lifetime.Singleton)]
    public class ExternalFundClassRepository : IFundClassRepository
    {
        private readonly BaseSettings _settings;

        private DateTime? _mainDataLastFailure;

        private FundDataResponseModel[] _data;

        private Dictionary<string, (FundDataResponseModel fundData, DateTime updatedDate)> _dataOnDemand;

        static bool isInitialized;

        private object locker = new object();

        private readonly IFundDataResponseModelPersistentCache _persistentCache;

        private readonly IConditionalErrorTracker _conditionalErrorTracker;

        private readonly double _individualFundCachingDuration;

        private readonly double _refreshDataAfterErrorIntervalInHours;


        public ExternalFundClassRepository(BaseSettings settings, IConditionalErrorTracker conditionalErrorTracker, IFundDataResponseModelPersistentCache persistentCache)
        {
            this._settings = settings;
            this._persistentCache = persistentCache;
            this._conditionalErrorTracker = conditionalErrorTracker;

            if (double.TryParse(_settings.GetSetting(Constants.Settings.IndividualFundCachingDuration), out var individualFundCachingDuration))
            {
                this._individualFundCachingDuration = individualFundCachingDuration;
            }

            if (double.TryParse(_settings.GetSetting(Constants.Settings.RefreshDataAfterErrorIntervalInHours), out var refreshDataAfterErrorIntervalInHours))
            {
                this._refreshDataAfterErrorIntervalInHours = refreshDataAfterErrorIntervalInHours;
            }

        }

        private void LoadData()
        {
            var fundDataForPriceType1 = GetDataFromAPI(null, Constants.PriceTypes.One, null, 20000);

            if (fundDataForPriceType1 == null)
            {
                fundDataForPriceType1 = _persistentCache.GetData(Constants.PriceTypes.One);
                _mainDataLastFailure = DateTime.UtcNow;
            }
            else
            {
                _persistentCache.SetData(fundDataForPriceType1, Constants.PriceTypes.One);

                var fundDataForPriceType0 = GetDataFromAPI(null, Constants.PriceTypes.Zero, null, 20000);
                if (fundDataForPriceType0 == null)
                {
                    _mainDataLastFailure = DateTime.UtcNow;
                }
                else
                {
                    _persistentCache.SetData(fundDataForPriceType0, Constants.PriceTypes.Zero);
                    _mainDataLastFailure = null;
                }
            }

            _data = fundDataForPriceType1;
        }

        public FundDataResponseModel[] GetData()
        {
            bool needRefresh = _mainDataLastFailure.HasValue && _mainDataLastFailure.Value.AddHours(_refreshDataAfterErrorIntervalInHours) < DateTime.UtcNow;
            if (_data == null || needRefresh)
            {
                lock (locker)
                {
                    if (_data == null || needRefresh)
                    {
                        LoadData();
                    }
                }
            }
            return _data;
        }

        public void UpdateData(bool force = false)
        {
            if (!isInitialized || force)
            {
                try
                {
                    lock (locker)
                    {
                        if (!isInitialized || force)
                        {
                            LoadData();

                            isInitialized = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _conditionalErrorTracker.Error($"Error updating External Fund Data: {ex.Message}", this);
                }
            }
        }

        public void SendEmailOnErrorForCiticode(string citicode)
        {
            _conditionalErrorTracker.TrySendEmail($"New data retrieval for citicode: {citicode} failed");
        }

        public FundDataResponseModel GetDataOnDemand(string citicode, string priceType = Constants.PriceTypes.One, string currency = "")
        {
            var dictionaryKey = citicode + priceType + currency;

            if (_dataOnDemand == null)
            {
                _dataOnDemand = new Dictionary<string, (FundDataResponseModel fundData, DateTime updatedDate)>();
            }

            var shouldLoadFundFromCache = _dataOnDemand.ContainsKey(dictionaryKey) &&
                                          _dataOnDemand[dictionaryKey].fundData != null &&
                                          _dataOnDemand[dictionaryKey].updatedDate.AddHours(_individualFundCachingDuration) > DateTime.UtcNow; 

            if (shouldLoadFundFromCache) 
            {
                return _dataOnDemand[dictionaryKey].fundData;
            }

            var fundData = GetDataFromAPI(citicode, priceType, currency, 10000)?.FirstOrDefault();

            if (fundData == null)
            {
                fundData = _persistentCache.GetData(citicode, priceType, currency);
            }
            else
            {
                _persistentCache.SetData(fundData, citicode, priceType, currency);
            }
           
            _dataOnDemand[dictionaryKey] = (fundData, DateTime.UtcNow) ;

            return fundData;
        }

        private bool IsValidCurrency(string currency)
        {
            var invalidCurrencies = _settings.GetSetting(Constants.Settings.InvalidCurrencies)?.Split(',');

            return !string.IsNullOrEmpty(currency) && !invalidCurrencies.Any(x => currency.Equals(x, StringComparison.CurrentCultureIgnoreCase));
        }

        private FundDataResponseModel[] GetDataFromAPI(string citicode, string priceType, string currency, int timeout)
        {
            var url = _settings.GetSetting(Constants.Settings.FeApiEndPoint);
            if (string.IsNullOrEmpty(url))
            {
                _conditionalErrorTracker.Error("No FE API end point", this);
                return null;
            }

            var client = new RestClient(url);
            var request = new RestRequest("/api/funddata/liontrust/C9E1ACC5-B0E7-400A-A5BF-83C68654EA0B");
            request.AddQueryParameter("rangename", "LiontrustFunds");
            if (citicode != null) 
            { 
                request.AddQueryParameter("citicodes", citicode); 
            }
            request.AddQueryParameter("pricetype", priceType);
            if (currency != null && IsValidCurrency(currency))
            {
                request.AddQueryParameter("currency", currency);
            }

            request.Timeout = timeout;
            var result = client.Execute(request);
            string requestUrl = $"{url}{request.Resource}?{string.Join("&", request.Parameters.Select(x => $"{x.Name}={x.Value}"))}";
            Log.Info($"[ExternalFund]: The API has been called: {requestUrl}", this);

            if (!result.IsSuccessful)
            {
                var errorMessage = $"There was an error during the retrieval of External Fund Data for {requestUrl}";
                _conditionalErrorTracker.Error(errorMessage, this);
                return null;
            }

            var fundDetails = JsonConvert.DeserializeObject<FundDataResponseModel[]>(result.Content);
            if (fundDetails == null || !fundDetails.Any())
            {
                var errorMessage = $"There was no External Fund Data retrieved for {requestUrl}.";
                _conditionalErrorTracker.Error(errorMessage, this);
                return null;
            }

            _conditionalErrorTracker.Success();
            return fundDetails;
        }
    }
}