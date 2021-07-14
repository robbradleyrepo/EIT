namespace LionTrust.Foundation.Search.DI
{
    using LionTrust.Foundation.Search.Repositories.Implementations;
    using LionTrust.Foundation.Search.Repositories.Interfaces;
    using LionTrust.Foundation.Search.Services.Implementations;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IArticleContentSearchService, ArticleContentSearchService>();
            serviceCollection.AddTransient<IArticleContentSearchRepository, ArticleContentSearchRepository>();
            serviceCollection.AddTransient<IFundContentSearchService, FundContentSearchService>();
            serviceCollection.AddTransient<IFundContentSearchRepository, FundContentSearchRepository>();
            serviceCollection.AddTransient<IGenericContentSearchService, GenericContentSearchService>();
            serviceCollection.AddTransient<IGenericContentSearchRepository, GenericContentSearchRepository>();
        }
    }
}
