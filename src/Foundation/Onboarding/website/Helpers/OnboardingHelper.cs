namespace LionTrust.Foundation.Onboarding.Helpers
{
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Abstractions;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model.Entities;
    using static LionTrust.Foundation.Onboarding.Constants;
    using System.Linq;
    using System.Collections.Generic;
    using System;

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
            var investorType = InvestorType.Private;

            var tracker = Tracker.Current;
            if (!IsValidConfiguration(onboardingConfiguration, log))
            {
                return investorType;
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
                            if (profile.PatternId == onboardingConfiguration.PrivatePatternCard.Id)
                            {
                                investorType = InvestorType.Private;
                            }
                            else if (profile.PatternId == onboardingConfiguration.ProfressionalPatternCard.Id)
                            {
                                investorType = InvestorType.Professional;
                            }
                        }
                    }
                }
            }

            return investorType;
        }

        public static bool HasAccess(IEnumerable<ICountry> excludedCountries)
        {
            var hasAccess = true;

            if (excludedCountries != null && excludedCountries.Any())
            {
                hasAccess = HasAccess(excludedCountries.Select(c => c.CountryName));
            }

            return hasAccess;
        }

        public static bool HasAccess(IEnumerable<string> excludedCountries)
        {
            var hasAccess = true;

            if (excludedCountries != null && excludedCountries.Any())
            {
                var address = GetCurrentContactAddress();

                if (address != null
                    && excludedCountries.Any(e => e.Equals(address.Country, StringComparison.InvariantCultureIgnoreCase)))
                {
                    hasAccess = false;
                }
            }

            return hasAccess;
        }

        public static IAddress GetCurrentContactAddress(bool createIfNull = false)
        {
            IAddress address = null;
          

            if (Tracker.Current != null &&  Tracker.Current.Contact != null)
            {
                var contact = Tracker.Current.Contact;
                var addresses = contact.GetFacet<IContactAddresses>(Analytics.Addresses_FacetName);

                if (addresses != null && addresses.Entries.Contains(Analytics.DefaultAddress_EntityName))
                {
                    address = addresses.Entries[Analytics.DefaultAddress_EntityName];
                }
                else if (addresses != null && createIfNull)
                {
                    address = addresses.Entries.Create(Analytics.DefaultAddress_EntityName);
                }
            }
            
            return address;
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