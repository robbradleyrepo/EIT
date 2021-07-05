namespace LionTrust.Feature.Navigation.DI
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Navigation.Controllers;
    using LionTrust.Feature.Navigation.Repositories;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Abstractions;
    using Sitecore.DependencyInjection;
    using Sitecore.Mvc.Presentation;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(provider => new NavigationController(new NavigationRepository(RenderingContext.Current.ContextItem), provider.GetService<IMvcContext>(), provider.GetService<BaseLog>()));
        }
    }
}
