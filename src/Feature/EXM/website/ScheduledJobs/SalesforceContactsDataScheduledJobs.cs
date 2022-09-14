namespace LionTrust.Feature.EXM.ScheduledJobs
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Data.Items;
    using Sitecore.Tasks;
    using Sitecore.DependencyInjection;
    using LionTrust.Feature.EXM.Services.Interfaces;
    using Glass.Mapper.Sc;
    using LionTrust.Feature.EXM.Models;

    public class SalesforceContactsDataScheduledJobs
    {
        public async System.Threading.Tasks.Task UpdateData(Item[] itemArray, CommandItem commandItem, ScheduleItem scheduleItem)
        {
            if (itemArray.Length == 0)
            {
                return;
            }

            var sitecoreService = new SitecoreService(itemArray[0].Database);
            var settings = sitecoreService.GetItem<ISalesforceSyncSettings>(itemArray[0]);

            if (!settings.Enabled)
            {
                return;
            }

            var service = ServiceLocator.ServiceProvider.GetService<ISalesforceAnalyticsService>();
            await service.RunSyncProcess();
        }
    }
}