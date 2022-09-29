namespace LionTrust.Feature.EXM.DI
{
    using Glass.Mapper.Sc;
    using LionTrust.Feature.EXM.Helpers.Implementations;
    using LionTrust.Feature.EXM.Helpers.Interfaces;
    using LionTrust.Feature.EXM.Repositories.Implementations;
    using LionTrust.Feature.EXM.Repositories.Interfaces;
    using LionTrust.Feature.EXM.Services.Implementations;
    using LionTrust.Feature.EXM.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IArticleRepository, ArticleRepository>();
            serviceCollection.AddTransient<IFillEmailHelper, FillMailHelper>();
            serviceCollection.AddTransient<IEmailService, SendGridEmailService>();
            serviceCollection.AddTransient<ICustomContactService, CustomContactService>();
            serviceCollection.AddTransient<ISalesforceAnalyticsService, SalesforceAnalyticsService>(provider => new SalesforceAnalyticsService(new SitecoreService("master")));
        }
    }
}
