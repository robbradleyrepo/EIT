namespace LionTrust.Feature.MyPreferences.DI
{
    using LionTrust.Feature.MyPreferences.Repositories;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IApplicationCacheRepository, ApplicationCacheRepository>();
            serviceCollection.AddTransient<IEmailPreferencesRepository, EmailPreferencesRepository>();
        }
    }
}
