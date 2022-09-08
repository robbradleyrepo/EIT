using LionTrust.Feature.EXM.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LionTrust.Feature.EXM.Controllers
{
    [ServicesController]
    [Authorize]
    public class SalesforceSyncController : ServicesApiController
    {
        private ISalesforceAnalyticsService _salesforceAnalyticsService;

        public SalesforceSyncController(
          ISalesforceAnalyticsService salesforceAnalyticsService)
          : base()
        {
            _salesforceAnalyticsService = salesforceAnalyticsService;
        }

        public SalesforceSyncController()
          : this(ServiceLocator.ServiceProvider.GetService<ISalesforceAnalyticsService>())
        {
        }

        [HttpGet]
        [ActionName("ExplicitlyRunSyncProcess")]
        public async Task<bool> ExplicitlyRunSyncProcess()
        {
            return await _salesforceAnalyticsService.RunSyncProcess();
        }
    }
}