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
    using System.Globalization;
    using Glass.Mapper.Sc.Web.Mvc;
    using Sitecore.XConnect;
    using Sitecore.Diagnostics;
    using Sitecore.XConnect.Collection.Model;
    using Sitecore.XConnect.Client;
    using System.Xml.Linq;
    using System.Xml;
    using Sitecore.Analytics.Tracking;
    using Contact = Sitecore.XConnect.Contact;
    using Sitecore.Analytics.Data;
    using Sitecore.Analytics.Model;
    using System.Web;

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

        public static IInvestor GetCurrentContactInvestor(IMvcContext context, BaseLog log)
        {
            var home = context.GetHomeItem<IHome>();
            IInvestor inverstor = null;
            var tracker = Tracker.Current;
            if (home == null || home.OnboardingConfiguration == null || !IsValidConfiguration(home.OnboardingConfiguration, log))
            {
                return null;
            }

            if (tracker != null && tracker.Interaction != null
                && tracker.Interaction.Profiles != null)
            {
                if (tracker.Interaction.Profiles.ContainsProfile(home.OnboardingConfiguration.Profile.Name))
                {
                    var profile = tracker.Interaction.Profiles[home.OnboardingConfiguration.Profile.Name];
                    if (profile.PatternId != null && profile.PatternId.Value != null)
                    {
                        if (profile.PatternId.HasValue)
                        {
                            inverstor = home.OnboardingConfiguration.ChooseInvestorRole?
                                .FirstOrDefault()?
                                .Investors?
                                .FirstOrDefault(i => i?.PatternCard.Id == profile.PatternId);
                        }
                    }
                }
            }

            return inverstor;
        }

        public static bool HasAccess(IEnumerable<ICountry> excludedCountries)
        {
            var hasAccess = true;

            if (excludedCountries != null && excludedCountries.Any())
            {
                hasAccess = HasAccess(excludedCountries.Select(c => c.ISO));
            }

            return hasAccess;
        }

        public static ICountry GetCurrentContactCountry(IMvcContext context)
        {
            var home = context.GetHomeItem<IHome>();

            using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
            {
                var contact = GetContact(client);

                if (string.IsNullOrWhiteSpace(contact?.Addresses()?.PreferredAddress?.CountryCode) || home == null || home.OnboardingConfiguration == null)
                {
                    return null;
                }

                return GetCountryFromIso(context, contact.Addresses().PreferredAddress.CountryCode);
            }
        }

        public static ICountry GetCountryFromIso(IMvcContext context, string twoLetterISO)
        {
            var home = context.GetHomeItem<IHome>();

            if (home == null)
            {
                return null;
            }

            var countries = home.OnboardingConfiguration.ChooseCountry?
                .SelectMany(x => x.Regions?.SelectMany(c => c.Countries));

            if (countries == null)
            {
                return null;
            }

            var country = countries.FirstOrDefault(c => c.ISO == twoLetterISO);

            if (country == null || country.ISO == Country.RestOfWorldIso)
            {
                country = countries.FirstOrDefault(c => c.ISO == Country.RestOfWorldIso);
            }

            return country;
        }

        public static bool HasAccess(IEnumerable<string> excludedCountries)
        {
            var hasAccess = true;

            if (excludedCountries != null && excludedCountries.Any())
            {
                //var address = GetCurrentContactAddress();
                //Get contact here

                using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                {
                    var contact = GetContact(client);

                    if (excludedCountries.Any(e => e.Equals(contact?.Addresses()?.PreferredAddress?.CountryCode, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        hasAccess = false;
                    }
                }
            }

            return hasAccess;
        }

        public static string GetCurrentContactCountryCode()
        {
            var countryCode = string.Empty;

            using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
            {
                var contact = GetContact(client);

                countryCode = contact?.Addresses()?.PreferredAddress?.CountryCode;
            }

            return countryCode;
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

            return true;
        }

        public static Contact GetContact(XConnectClient client)
        {
            var trackerIdentifier = new IdentifiedContactReference("xDB.Tracker", Tracker.Current.Contact.ContactId.ToString("N"));

            Contact contact = null;

            try
            {
                contact = client.Get(trackerIdentifier, new ContactExpandOptions(AddressList.DefaultFacetKey));
            }
            catch (Exception ex)
            {
                Log.Error("Error trying to get contact", ex);
            }

            return contact;
        }

        public static void AddPointsFromProfileCard(IOnboardingConfiguration configuration, IProfileCard profileCard)
        {
            var scores = new Dictionary<string, double>();

            var profile = GetProfile(configuration.Profile.Name, true);

            if (profileCard != null && !string.IsNullOrEmpty(profileCard.ProfileCardValue))
            {
                try
                {
                    var xmlData = XDocument.Parse(profileCard.ProfileCardValue);
                    var xmlDoc = xmlData;

                    if (xmlDoc != null)
                    {
                        var parentNode = xmlDoc.Descendants(Analytics.ProfileCardValueKey_XmlElementName);

                        if (parentNode != null)
                        {
                            foreach (var childrenNode in parentNode)
                            {
                                if (childrenNode.HasAttributes
                                    && childrenNode.Attribute(Analytics.ProfileCardValueName_XmlAttribute) != null && !string.IsNullOrWhiteSpace(childrenNode.Attribute(Analytics.ProfileCardValueName_XmlAttribute).Value)
                                    && childrenNode.Attribute(Analytics.ProfileCardValueValue_XmlAttribute) != null && !string.IsNullOrWhiteSpace(childrenNode.Attribute(Analytics.ProfileCardValueValue_XmlAttribute).Value))
                                {
                                    scores.Add(childrenNode.Attribute(Analytics.ProfileCardValueName_XmlAttribute).Value, Convert.ToDouble(childrenNode.Attribute(Analytics.ProfileCardValueValue_XmlAttribute).Value));
                                }
                            }

                            if (scores.Any(s => s.Value > 0))
                            {
                                profile.Score(scores);
                            }

                            // update the pattern based on the scores you updated - this is supposed to be called from Score as well
                            // but doesn't always update unless you call it explicitly
                            profile.UpdatePattern();
                        }
                    }
                }
                catch (XmlException ex)
                {
                    Log.Error($"Xml for profile card is invalid. Value is {profileCard.ProfileCardValue}", ex);
                }
            }
        }

        public static Profile GetProfile(string profileName, bool createNew = false)
        {
            Profile profile = null;

            if (Tracker.Current != null && Tracker.Current.Interaction != null && Tracker.Current.Interaction.Profiles != null)
            {

                if (Tracker.Current.Interaction.Profiles.ContainsProfile(profileName))
                {
                    if (createNew)
                    {
                        Tracker.Current.Interaction.Profiles.Remove(profileName);
                    }
                    else
                    {
                        profile = Tracker.Current.Interaction.Profiles[profileName];
                    }
                }

                if (profile == null && createNew)
                {
                    var listOfProfileData = new List<ProfileData>()
                    {
                        new ProfileData(profileName)
                    };

                    Tracker.Current.Interaction.Profiles.Initialize(listOfProfileData);
                    profile = Tracker.Current.Interaction.Profiles[profileName];
                }
            }

            return profile;
        }

        public static string GetChangeUrl()
        {
            if(HttpContext.Current == null)
            {
                return string.Empty;
            }

            var uriBuilder = new UriBuilder(HttpContext.Current.Request.Url.ToString());
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add(QueryStringNames.Change, bool.TrueString.ToLower());
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri.PathAndQuery;
        }

        public static bool IsUkResident()
        {
            return GetCurrentContactCountryCode()?.Equals(CountryCodes.UK, StringComparison.InvariantCultureIgnoreCase) ?? false;
        }
    }
}