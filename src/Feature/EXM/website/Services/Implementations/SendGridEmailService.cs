using LionTrust.Feature.EXM.Services.Interfaces;
using Newtonsoft.Json;
using SendGrid;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace LionTrust.Feature.EXM.Services.Implementations
{
    public class SendGridEmailService : IEmailService
    {
        private readonly string _apiKey;

        public SendGridEmailService(string password)
        {
            _apiKey = password;
        }

        public async Task<List<Models.Bounce>> GetBlocks()
        {
            var softBounces = new List<Models.Bounce>();

            var response = await GetResponse(BaseClient.Method.GET, Constants.SendGridApi.Blocks);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                softBounces = JsonConvert.DeserializeObject<List<Models.Bounce>>(response.Body.ReadAsStringAsync().Result);
            }

            return softBounces;
        }

        public async Task<bool> DeleteBlock(string email)
        {
            var response = await GetResponse(BaseClient.Method.DELETE, $"{Constants.SendGridApi.Blocks}/{email}");

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<List<Models.Bounce>> GetBounces()
        {
            var bounces = new List<Models.Bounce>();

            var response = await GetResponse(BaseClient.Method.GET, Constants.SendGridApi.Bounces);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                bounces = JsonConvert.DeserializeObject<List<Models.Bounce>>(response.Body.ReadAsStringAsync().Result);
            }

            return bounces;
        }

        public async Task<bool> DeleteBounce(string email)
        {
            var queryParams = $"{{'email_address': '{email}'}}";
            var response = await GetResponse(BaseClient.Method.DELETE, $"{Constants.SendGridApi.Bounces}/{email}", queryParams);

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        private async Task<Response> GetResponse(BaseClient.Method method, string urlPath, string queryParams = null)
        {
            var headers = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
            };

            var client = new SendGridClient(apiKey: _apiKey, requestHeaders: headers);

            var response = await client.RequestAsync(
                method: method,
                urlPath: urlPath,
                queryParams: queryParams
            );

            return response;
        }
    }
}