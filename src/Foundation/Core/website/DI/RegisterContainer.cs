using LionTrust.Foundation.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace LionTrust.Foundation.Core.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMediatorService, MediatorService>();
        }
    }
}
