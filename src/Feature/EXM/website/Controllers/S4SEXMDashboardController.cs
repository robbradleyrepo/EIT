using LionTrust.Feature.EXM.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Services.Core;
using Sitecore.Services.Core.Model;
using Sitecore.Services.Infrastructure.Sitecore.Services;
using System;
using System.Web.Http;

namespace LionTrust.Feature.EXM.Controllers
{
    [ServicesController]
    [Authorize]
    public class S4SEXMDashboardController : EntityService<EntityIdentity>
    {
        private ISalesforceAnalyticsService _salesforceAnalyticsService;

        public S4SEXMDashboardController(
          ISalesforceAnalyticsService salesforceAnalyticsService)
          : base(null)
        {
            _salesforceAnalyticsService = salesforceAnalyticsService;
        }

        public S4SEXMDashboardController()
          : this(ServiceLocator.ServiceProvider.GetService<ISalesforceAnalyticsService>())
        {
        }

        [HttpPost]
        [ActionName("ExplicitlyRunSyncProcess")]
        public void ExplicitlyRunSyncProcess(string id)
        {
            var date = DateTime.MinValue.ToUniversalTime();
            var contacts = _salesforceAnalyticsService.GetContactWithInteractions(date);
            var sfEntities = _salesforceAnalyticsService.GetSalesforceEntities(contacts);
            //_salesforceAnalyticsService.SyncData(sfEntities);
        }
    }
}