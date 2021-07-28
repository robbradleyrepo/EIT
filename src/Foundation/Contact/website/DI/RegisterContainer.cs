namespace LionTrust.Foundation.Contact.DI
{
    using LionTrust.Foundation.Contact.Services;
    using LionTrust.Foundation.Contact.Repositories;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMailService, MailService>();
            serviceCollection.AddTransient<IApplicationCacheRepository, ApplicationCacheRepository>();
            serviceCollection.AddTransient<ILabelsRepository, LabelsRepository>();
            serviceCollection.AddTransient<IEmailPreferencesRepository, EmailPreferencesRepository>();
            serviceCollection.AddTransient<IEntityFactory, EntityFactory>();
            serviceCollection.AddTransient<IBaseRepository, BaseRepository>();
        }
    }
}
