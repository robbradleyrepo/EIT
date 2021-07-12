using Sitecore.Analytics;

namespace LionTrust.Feature.Onboarding
{
    public interface ITrackerResolver
    {
        ITracker GetTracker();
    }
}
