namespace LionTrust.Foundation.SitecoreExtensions.DI
{
    using LionTrust.Foundation.SitecoreExtensions.Placeholders;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<CustomBasePlaceholderCacheManager, CustomBasePlaceholderCacheManager>();
        }
    }
}
