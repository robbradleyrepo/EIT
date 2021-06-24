namespace LionTrust.Feature.Navigation.Helpers
{
    using LionTrust.Feature.Navigation.Models;
    using Sitecore.Analytics;
    using System;

    public static class OnboardingHelper
    {
        public static string ProfileRoleName(string profileName, string privateId, string professionalId)
        {
            var tracker = Tracker.Current;
            if (tracker != null && tracker.Interaction != null && tracker.Interaction.Profiles != null)
            {
                if (tracker.Interaction.Profiles.ContainsProfile(profileName))
                {
                    var profile = tracker.Interaction.Profiles[profileName];
                    if (profile.PatternId.Value != null)
                    {
                        if (profile.PatternId.Value.Equals(new Guid(privateId)) || profile.PatternId.Value.Equals(new Guid(professionalId)))
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