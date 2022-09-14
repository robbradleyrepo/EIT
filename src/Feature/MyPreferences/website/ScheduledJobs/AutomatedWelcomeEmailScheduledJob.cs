namespace LionTrust.Feature.MyPreferences.ScheduledJobs
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Data.Items;
    using Sitecore.Tasks;
    using Sitecore.DependencyInjection;
    using Glass.Mapper.Sc;
    using LionTrust.Feature.MyPreferences.Models;
    using LionTrust.Feature.MyPreferences.Services;

    public class AutomatedWelcomeEmailScheduledJob
    {
        public async System.Threading.Tasks.Task Execute(Item[] itemArray, CommandItem commandItem, ScheduleItem scheduleItem)
        {
            if (itemArray.Length == 0)
            {
                return;
            }

            var sitecoreService = new SitecoreService("web");
            var settings = sitecoreService.GetItem<IAutomatedWelcomeSettings>(itemArray[0]);

            if (!settings.Enabled)
            {
                return;
            }

            Sitecore.Diagnostics.Log.Info("[AutomatedWelcomeEmail] Starting job...", this);

            var registerInvestor = sitecoreService.GetItem<IRegisterInvestor>(Constants.RegisterInvestor.Item_ID);

            var emailPreferencesService = ServiceLocator.ServiceProvider.GetService<IEmailPreferencesService>();
            emailPreferencesService.SendAutomatedWelcomeEmails(registerInvestor, settings);

            Sitecore.Diagnostics.Log.Info("[AutomatedWelcomeEmail] Finished job", this);
        }
    }
}