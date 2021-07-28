namespace LionTrust.Foundation.Onboarding.Helpers
{
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Abstractions;
    using Sitecore.Analytics;
    using static LionTrust.Foundation.Onboarding.Constants;

    public static class OnboardingHelper
    {
        public static string ProfileRoleName(IOnboardingConfiguration onboardingConfiguration, BaseLog log)
        {
            var tracker = Tracker.Current;
            if (!IsValidConfiguration(onboardingConfiguration, log))
            {
                return string.Empty;
            }

            if (tracker != null && tracker.Interaction != null 
                && tracker.Interaction.Profiles != null)
            {
                if (tracker.Interaction.Profiles.ContainsProfile(onboardingConfiguration.Profile.Name))
                {
                    var profile = tracker.Interaction.Profiles[onboardingConfiguration.Profile.Name];                    
                    if (profile.PatternId != null && profile.PatternId.Value != null)
                    {
                        if (profile.PatternId.HasValue)
                        {
                            return profile.PatternLabel;
                        }
                    }                    
                }
            }

            return string.Empty;
        }

        public static InvestorType GetInvestorType(IOnboardingConfiguration onboardingConfiguration, BaseLog log)
        {
            //TODO: logic to return correct investor type.
            return InvestorType.Private;
        }

        private static bool IsValidConfiguration(IOnboardingConfiguration configuration, BaseLog log)
        {
            if (configuration == null)
            {
                log.Error("Configuration not found", log);
                return false;
            }

            if (configuration.Profile == null)
            {
                log.Error("Onboarding configuration has no profile set", configuration);
                return false;
            }

            if (configuration.PrivateProfileCard == null)
            {
                log.Error("Onboarding configuration has no private profile card set", configuration);
                return false;
            }

            if (configuration.ProfressionalProfileCard == null)
            {
                log.Error("Onboarding configuration has no professional profile card set", configuration);
                return false;
            }

            return true;
        }
    }
}