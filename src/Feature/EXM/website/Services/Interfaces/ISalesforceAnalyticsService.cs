using System.Threading.Tasks;

namespace LionTrust.Feature.EXM.Services.Interfaces
{
    public interface ISalesforceAnalyticsService
    {
        Task<bool> RunSyncProcess();
    }
}
