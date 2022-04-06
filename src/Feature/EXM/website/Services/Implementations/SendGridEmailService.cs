using LionTrust.Feature.EXM.Services.Interfaces;
using Newtonsoft.Json;
using SendGrid;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LionTrust.Feature.EXM.Services.Implementations
{
    public class SendGridEmailService : IEmailService
    {
        private readonly string _apiKey;

        public SendGridEmailService()
        {
            _apiKey = Sitecore.Configuration.Settings.GetSetting(Constants.Settings.MailServerPassword);
        }

        public async Task<List<Models.Bounce>> GetBlocks()
        {
            List<Models.Bounce> softBounces = new List<Models.Bounce>();

            var headers = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
            };

            var client = new SendGridClient(apiKey: _apiKey, requestHeaders: headers);

            var response = await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: "suppression/blocks"
            );

            if (response.StatusCode == HttpStatusCode.OK)
            {
                softBounces = JsonConvert.DeserializeObject<List<Models.Bounce>>(response.Body.ReadAsStringAsync().Result);
            }

            return softBounces;
        }

        public async Task<bool> DeleteBlock(string email)
        {
            var client = new SendGridClient(_apiKey);
            var response = await client.RequestAsync(
                method: SendGridClient.Method.DELETE,
                urlPath: $"suppression/blocks/{email}"
            );

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<List<Models.Bounce>> GetBounces()
        {
            List<Models.Bounce> bounces = new List<Models.Bounce>();

            var headers = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
            };

            var client = new SendGridClient(apiKey: _apiKey, requestHeaders: headers);

            var response = await client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: "suppression/bounces"
            );

            if (response.StatusCode == HttpStatusCode.OK)
            {
                bounces = JsonConvert.DeserializeObject<List<Models.Bounce>>(response.Body.ReadAsStringAsync().Result);
            }

            return bounces;
        }

        public async Task<bool> DeleteBounce(string email)
        {
            var queryParams = $"{{'email_address': '{email}'}}";

            var client = new SendGridClient(_apiKey);
            var response = await client.RequestAsync(
                method: SendGridClient.Method.DELETE,
                urlPath: $"suppression/bounces/{email}",
                queryParams: queryParams
            );

            return response.StatusCode == HttpStatusCode.NoContent;
        }
    }
}