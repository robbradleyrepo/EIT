namespace LionTrust.Feature.Fund.Api.Cache
{
    using LionTrust.Foundation.DI;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;

    [Service(ServiceType = typeof(IFundDataResponseModelPersistentCache), Lifetime = Lifetime.Singleton)]
    public class FundDataResponseModelPersistentCache : IFundDataResponseModelPersistentCache
    {
        private string _rootFolderPath;

        public FundDataResponseModelPersistentCache()
        {
            string dataFolder = Sitecore.IO.FileUtil.MapPath(Sitecore.Configuration.Settings.DataFolder);
            _rootFolderPath = $"{dataFolder}/{typeof(FundDataResponseModel).Name}";

            if(!Directory.Exists(_rootFolderPath))
            {
                Directory.CreateDirectory(_rootFolderPath);
            }
        }

        public FundDataResponseModel[] GetData(string priceType = Constants.PriceTypes.One)
        {
            string arrayFilePath = GetArrayFilePath(priceType);
            string text = File.ReadAllText(arrayFilePath);
            return JsonConvert.DeserializeObject<FundDataResponseModel[]>(text);
        }

        public FundDataResponseModel GetData(string citicode, string priceType = Constants.PriceTypes.One, string currency = "")
        {
            string filePath = GetFilePath(citicode, priceType, currency);
            string text = File.ReadAllText(filePath);
            var result = JsonConvert.DeserializeObject<FundDataResponseModel>(text);

            string arrayFilePath = GetArrayFilePath(priceType);

            if (File.Exists(arrayFilePath) && File.GetLastWriteTime(arrayFilePath) > File.GetLastWriteTime(filePath))
            {
                result = GetData().FirstOrDefault(d => d.CitiCode == result.CitiCode && (string.IsNullOrEmpty(currency) || d.UnitCurrency == currency)) ?? result;
            }

            return result;
        }

        public void SetData(FundDataResponseModel[] data, string priceType = Constants.PriceTypes.One)
        {
            string arrayFilePath = GetArrayFilePath(priceType);
            string text = JsonConvert.SerializeObject(data);
            File.WriteAllText(arrayFilePath, text);
        }

        public void SetData(FundDataResponseModel data, string citicode, string priceType = Constants.PriceTypes.One, string currency = "")
        {
            string filePath = GetFilePath(citicode, priceType, currency);
            string text = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, text);
        }

        private string GetArrayFilePath(string priceType = Constants.PriceTypes.One)
        {
            string fileName = GetValidFileName($"_{priceType}");
            return $"{_rootFolderPath}/{fileName}.json";
        }

        private string GetFilePath(string citicode, string priceType = Constants.PriceTypes.One, string currency = "")
        {
            string fileName = GetValidFileName($"{citicode}-{priceType}-{currency}");
            return $"{_rootFolderPath}/{fileName}.json";
        }

        private string GetValidFileName(string fileName)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '-');
            }
            return fileName;
        }
    }
}