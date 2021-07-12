namespace LionTrust.Feature.Onboarding
{
    using LionTrust.Foundation.DI;
    using Sitecore.Analytics;

    [Service(ServiceType = typeof(ITrackerResolver), Lifetime = Lifetime.Singleton)]
    public class TrackerResolver : ITrackerResolver
    {
        public ITracker GetTracker()
        {
            return Tracker.Current;
        }
    }
}