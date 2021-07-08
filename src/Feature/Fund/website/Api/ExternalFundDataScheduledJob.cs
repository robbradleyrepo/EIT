namespace LionTrust.Feature.Fund.Api
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Data.Items;
    using Sitecore.Tasks;
    using Sitecore.DependencyInjection;    

    public class ExternalFundDataScheduledJob
    {
        public void UpdateData(Item[] itemArray, CommandItem commandItem, ScheduleItem scheduleItem)
        {
            try
            {
                var fundRepo = ServiceLocator.ServiceProvider.GetService<IFundClassRepository>();
                fundRepo.UpdateData();

            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error updating External Fund Data: ", ex, this);
            }
        }
    }
}