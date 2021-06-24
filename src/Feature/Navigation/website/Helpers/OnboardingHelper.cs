namespace LionTrust.Feature.Navigation.Helpers
{
    using LionTrust.Feature.Navigation.Models;
    using Sitecore.Analytics;

    public static class OnboardingHelper
    {
        public static OnboardingRole ProfileRole(string profileName, string privateId, string professionalId)
        {
            var tracker = Tracker.Current;
            if (tracker != null && tracker.Interaction != null && tracker.Interaction.Profiles != null)
            {
                if (tracker.Interaction.Profiles.ContainsProfile(profileName))
                {
                    var profile = tracker.Interaction.Profiles[profileName];
                    if (profile.PatternId != null)
                    {
                        if (profile.PatternId.Value.ToString().Equals(privateId))
                        {
                            return OnboardingRole.PrivateInvestor;
                        }
                        else if (profile.PatternId.Value.ToString().Equals(professionalId))
                        {
                            return OnboardingRole.ProfessionalInestor;
                        }
                    }                    
                }
            }

            return OnboardingRole.Null;
        }
    }
}