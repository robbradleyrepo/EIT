namespace LionTrust.Foundation.SitecoreForms.DI
{
    using LionTrust.Foundation.SitecoreForms.Factories;
    using LionTrust.Foundation.SitecoreForms.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISitecoreFormsCustomSaveActionRepository, SitecoreFormsCustomSaveActionRepository>();
            serviceCollection.AddTransient<ISitecoreFormsCustomSaveActionsService, SitecoreFormsCustomSaveActionsService>();
        }
    }
}