namespace LionTrust.Foundation.Contact.DI
{
    using LionTrust.Foundation.Contact.Managers;
    using LionTrust.Foundation.Contact.Repositories;
    using LionTrust.Foundation.Contact.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMailManager, MailManager>();
            serviceCollection.AddTransient<IXConnectUtilityRepository, XConnectUtilityRepository>();
            serviceCollection.AddTransient<IPersonalizedContentPageRepository, PersonalizedContentPageRepository>();
            serviceCollection.AddTransient<IPersonalizedContentService, PersonalizedContentService>();
            serviceCollection.AddTransient<ISitecoreContactUtility, SitecoreContactUtility>();
            serviceCollection.AddTransient<ISFEntityUtility, SFEntityUtility>();
        }
    }
}
