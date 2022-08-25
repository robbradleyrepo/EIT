using Glass.Mapper.Sc;
using LionTrust.Feature.EXM.Models;
using LionTrust.Feature.EXM.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.SecurityModel;
using Sitecore.Services.Core;
using Sitecore.Services.Core.Model;
using Sitecore.Services.Infrastructure.Sitecore.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace LionTrust.Feature.EXM.Controllers
{
    [ServicesController]
    [Authorize]
    public class SalesforceSyncController : EntityService<EntityIdentity>
    {
        private ISalesforceAnalyticsService _salesforceAnalyticsService;
        private ISitecoreService _sitecoreService;

        public SalesforceSyncController(
          ISalesforceAnalyticsService salesforceAnalyticsService, ISitecoreService sitecoreService)
          : base(null)
        {
            _salesforceAnalyticsService = salesforceAnalyticsService;
            _sitecoreService = sitecoreService;
        }

        public SalesforceSyncController()
          : this(ServiceLocator.ServiceProvider.GetService<ISalesforceAnalyticsService>(), ServiceLocator.ServiceProvider.GetService<ISitecoreService>())
        {
        }

        [HttpGet]
        [ActionName("ExplicitlyRunSyncProcess")]
        public async Task ExplicitlyRunSyncProcess(string id)
        {
            var settings = _sitecoreService.GetItem<ISalesforceSyncSettings>(Constants.SalesforceSyncSettings.Id);
            var lastSyncDate = (settings.LastSyncDate.HasValue ? settings.LastSyncDate.Value : DateTime.MinValue).ToUniversalTime();
            var entities = await _salesforceAnalyticsService.GetEntityWithInteractions(lastSyncDate);
            //_salesforceAnalyticsService.SyncEngagementHistory(entities);
            //_salesforceAnalyticsService.SyncScore(entities);

            if (entities.SelectMany(x => x.Interactions).Any())
            {
                var lastRun = entities.SelectMany(x => x.Interactions).Max(x => x.Date);

                using (new SecurityDisabler())
                {
                    settings.LastSyncDate = lastRun;
                    _sitecoreService.SaveItem(new SaveOptions(settings));
                }
            }
        }
    }
}