namespace LionTrust.Feature.EXM.ScheduledJobs
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Data.Items;
    using Sitecore.Tasks;
    using Sitecore.DependencyInjection;
    using LionTrust.Feature.EXM.Services.Interfaces;
    using Sitecore.SecurityModel;
    using Glass.Mapper.Sc;
    using LionTrust.Feature.EXM.Models;
    using Log = Sitecore.Diagnostics.Log;
    using System.Linq;

    public class SalesforceContactsDataScheduledJobs
    {
        public void UpdateData(Item[] itemArray, CommandItem commandItem, ScheduleItem scheduleItem)
        {
            if (itemArray.Length == 0)
            {
                return;
            }

            var sitecoreService = new SitecoreService(itemArray[0].Database);
            var settings = sitecoreService.GetItem<ISalesforceSyncSettings>(itemArray[0]);

            try
            {
                var lastSyncDate = (settings.LastSyncDate.HasValue ? settings.LastSyncDate.Value : DateTime.MinValue).ToUniversalTime();

                var service = ServiceLocator.ServiceProvider.GetService<ISalesforceAnalyticsService>();

                var entities = service.GetEntityWithInteractions(lastSyncDate);
                //service.SyncEngagementHistory(entities);
                //service.SyncScore(entities);

                if (entities.SelectMany(x => x.Interactions).Any())
                {
                    var lastRun = entities.SelectMany(x => x.Interactions).Max(x => x.Date);

                    using (new SecurityDisabler())
                    {
                        settings.LastSyncDate = lastRun;
                        sitecoreService.SaveItem(new SaveOptions(settings));
                    }
                }

                Log.Debug(string.Format("[Salesforce Sync] Job finished: LastRun {0}", DateTime.Now.ToString("f")), this);

            }
            catch (Exception ex)
            {
                Log.Error("[Salesforce Sync] Error updating EXM data to Salesforce: ", ex, this);
            }
        }
    }
}