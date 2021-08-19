namespace LionTrust.Feature.DocumentUploader.DI
{
    using LionTrust.Feature.DocumentUploader.Repository;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IDocumentUploadRepository, DocumentUploadRepository>();
        }
    }
}