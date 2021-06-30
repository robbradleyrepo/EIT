namespace LionTrust.Foundation.Navigation.Helpers
{
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Analytics;

    public static class OnboardingHelper
    {
        public static string ProfileRoleName(IOnboardingConfiguration onboardingConfiguration)
        {
            var tracker = Tracker.Current;
            if (tracker != null && tracker.Interaction != null && tracker.Interaction.Profiles != null)
            {
                if (tracker.Interaction.Profiles.ContainsProfile(onboardingConfiguration.Profile.Name))
                {
                    var profile = tracker.Interaction.Profiles[onboardingConfiguration.Profile.Name];
                    if (profile.PatternId.Value != null)
                    {
                        if (profile.PatternId.Value.Equals(onboardingConfiguration.PrivateProfileCard.Id) || profile.PatternId.Value.Equals(onboardingConfiguration.ProfressionalProfileCard.Id))
                        {
                            return profile.PatternLabel;
                        }
                    }                    
                }
            }

            return string.Empty;
        }
    }
}