namespace LionTrust.Feature.Onboarding.Analytics
{
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Analytics.Tracking;

    public interface IProfileCardManager
    {
        void AddPointsFromProfileCard(IProfileCard profileCard, Profile profile);        
    }
}
