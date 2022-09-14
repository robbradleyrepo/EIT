namespace LionTrust.Feature.MyPreferences.DI
{
    using LionTrust.Feature.MyPreferences.Helpers;
    using LionTrust.Feature.MyPreferences.Repositories;
    using LionTrust.Feature.MyPreferences.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IApplicationCacheRepository, ApplicationCacheRepository>();
            serviceCollection.AddTransient<IEmailPreferencesRepository, EmailPreferencesRepository>();
            serviceCollection.AddTransient<IEmailPreferencesService, EmailPreferencesService>();
            serviceCollection.AddTransient<IEmailHelper, EmailHelper>();
        }
    }
}
