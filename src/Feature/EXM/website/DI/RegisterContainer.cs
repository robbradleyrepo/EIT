namespace LionTrust.Feature.EXM.DI
{
    using LionTrust.Feature.EXM.Repositories.Implementations;
    using LionTrust.Feature.EXM.Repositories.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IArticleRepository, ArticleRepository>();
        }
    }
}
