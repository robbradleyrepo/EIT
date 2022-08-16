namespace LionTrust.Feature.Navigation.DI
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Navigation.Controllers;
    using LionTrust.Feature.Navigation.Repositories;
    using LionTrust.Feature.Navigation.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Abstractions;
    using Sitecore.DependencyInjection;
    using Sitecore.Mvc.Presentation;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<INavigationRepository>(provider => new NavigationRepository(RenderingContext.Current.ContextItem));
            serviceCollection.AddTransient<INavigationService, NavigationService>();
        }
    }
}
