namespace LionTrust.Feature.Search.DI
{
    using LionTrust.Feature.Search.DataManagers.Implementations;
    using LionTrust.Feature.Search.DataManagers.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IArticleSearchDataManager, ArticleSearchDataManager>();
            serviceCollection.AddTransient<IFundSearchDataManager, FundSearchDataManager>();
        }
    }
}
