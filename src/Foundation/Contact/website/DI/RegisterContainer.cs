namespace LionTrust.Foundation.Contact.DI
{
    using LionTrust.Foundation.Contact.Managers;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMailManager, MailManager>();
        }
    }
}
