namespace LionTrust.Feature.Listings.DI
{
    using LionTrust.Feature.Listings.DataManagers.Implementations;
    using LionTrust.Feature.Listings.DataManagers.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGenericListingDataManager, GenericListingDataManager>();
        }
    }
}
