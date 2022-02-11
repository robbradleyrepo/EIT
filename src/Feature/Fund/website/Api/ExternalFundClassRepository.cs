﻿namespace LionTrust.Feature.Fund.Api
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Legacy.Models;
    using Newtonsoft.Json;
    using RestSharp;
    using Sitecore.Abstractions;
    using System;
    using System.Linq;

    [Service(ServiceType = typeof(IFundClassRepository), Lifetime = Lifetime.Singleton)]
    public class ExternalFundClassRepository : IFundClassRepository
    {
        private readonly BaseSettings _settings;

        private FundDataResponseModel[] _data;

        static bool isInitialized;

        private object locker = new object();

        private readonly IMailManager _mailManager;
        private readonly IMvcContext _context;

        public ExternalFundClassRepository(BaseSettings settings, IMailManager mailManager, IMvcContext context)
        {
            this._settings = settings;
            this._mailManager = mailManager;
            this._context = context;
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
                SendEmailOnError("There was an error during the retrieval of External Fund Data: " + result.ErrorMessage);
                return;
            }

            var fundDetails = JsonConvert.DeserializeObject<FundDataResponseModel[]>(result.Content);
            if (fundDetails == null || !fundDetails.Any())
            {
                SendEmailOnError("There was no External Fund Data retrieved.");
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

        public void UpdateData()
        {
            if (!isInitialized)
            {
                try
                {
                    lock (locker)
                    {
                        if (!isInitialized)
                        {
                            LoadData();

                            isInitialized = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    var message = "Error updating External Fund Data: ";
                    SendEmailOnError(message + ex.Message);
                    Sitecore.Diagnostics.Log.Error(message, ex, this);
                }
            }
        }

        public void SendEmailOnError(string errorMessage)
        {
            var emailTemplate = _context.SitecoreService.GetItem<IEmail>(Constants.EmailTemplates.FundImportErrorEmail);
            if (emailTemplate == null)
            {
                return;
            }

            _mailManager.SendEmail(emailTemplate.FromAddress, emailTemplate.FromDisplayName, emailTemplate.ToAddresses, emailTemplate.Subject, errorMessage, true);
        }

        public void SendEmailOnErrorForCiticode(string citicode)
        {
            SendEmailOnError("New data retrieval for citicode: " + citicode + " failed");
        }
    }
}