using LionTrust.Feature.Hero.Factories;
using LionTrust.Feature.Hero.Mediators;
using LionTrust.Feature.Hero.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace LionTrust.Feature.Hero.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IHeroMediator, HeroMediator>();
            serviceCollection.AddTransient<IHeroService, HeroService>();
            serviceCollection.AddTransient<IHeroViewModelFactory, HeroViewModelFactory>();
        }
    }
}
