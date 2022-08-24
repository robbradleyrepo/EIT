namespace LionTrust.Foundation.Onboarding.Helpers
{    
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Abstractions;
    using Sitecore.Analytics;
    using static LionTrust.Foundation.Onboarding.Constants;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using Glass.Mapper.Sc.Web.Mvc;
    using Sitecore.XConnect;
    using Sitecore.Diagnostics;
    using Sitecore.XConnect.Collection.Model;
    using Sitecore.XConnect.Client;
    using System.Xml.Linq;
    using System.Xml;
    using Sitecore.Analytics.Tracking;
    using Contact = Sitecore.XConnect.Contact;
    using Sitecore.Analytics.Model;
    using System.Web;
    using Sitecore.Web;
    using Sitecore.Analytics.Pipelines.InitializeInteractionProfile;
    using ContactIdentifier = Sitecore.Analytics.Model.Entities.ContactIdentifier;
    using LionTrust.Foundation.ORM.Models;

    public static class OnboardingHelper
    {
        public static string ProfileRoleName(IOnboardingConfiguration onboardingConfiguration, BaseLog log)
        {
            var profile = ProfileCard(onboardingConfiguration, log);

            if(profile == null)
            {
                return string.Empty;
            }

            return profile.PatternLabel;
        }

        public static string ViewingLabelWithArticle(string viewingLabel, string profileName)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };

            if (!string.IsNullOrEmpty(profileName) && !string.IsNullOrEmpty(viewingLabel))
            {
                char firstCharacter = profileName.ToLower().ToCharArray().ElementAt(0);
                if (vowels.Contains(firstCharacter))
                {
                    return $"{viewingLabel} an";
                }
                else
                {
                    return $"{viewingLabel} a";
                }
            }

            return viewingLabel;
        }

        public static IInvestor GetCurrentContactInvestor(IMvcContext context, BaseLog log)
        {
            var home = context.GetHomeItem<IHome>();
            IInvestor investor = null;
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
                            if (IsUkResident())
                            {
                                investor = home.OnboardingConfiguration.ChooseInvestorRole?
                                    .FirstOrDefault()?
                                    .Investors?
                                    .FirstOrDefault(i => i?.PatternCard.Id == profile.PatternId);
                            }
                            else
                            {
                                investor = home.OnboardingConfiguration.ChooseInvestorRole?
                                    .FirstOrDefault()?
                                    .Investors?
                                    .FirstOrDefault(i => i?.InternationalPatternCard.Id == profile.PatternId);
                            }
                        }
                    }
                }
            }

            return investor;
        }

        public static IInvestor GetOnboardingInvestor(IMvcContext context, BaseLog log)
        {
            IInvestor investor = null;
            var investorTypeId = OnboardingHelper.GetInvestorTypeId();

            if (investorTypeId.HasValue)
            {
                investor = context.SitecoreService.GetItem<IInvestor>(investorTypeId.Value);
            }

            if (investor == null)
            {
                investor = GetCurrentContactInvestor(context, log);

                if (investor != null)
                {
                    UpdateInvestorTypeSession(investor.Id);
                }
            }

            return investor;
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
                contact = (Contact)WebUtil.GetSessionValue(SessionKeys.Contact);

                if (contact == null)
                {
                    contact = client.Get(trackerIdentifier, new ContactExpandOptions(AddressList.DefaultFacetKey));
                    UpdateContactSession(contact);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error trying to get contact", ex, typeof(OnboardingHelper));
            }

            return contact;
        }

        public static void UpdateContactSession(Contact contact)
        {
            WebUtil.SetSessionValue(SessionKeys.Contact, contact);
        }

        public static Guid? GetInvestorTypeId()
        {
            return (Guid?)WebUtil.GetSessionValue(SessionKeys.InvestorType);
        }

        public static void UpdateInvestorTypeSession(Guid investorType)
        {
            WebUtil.SetSessionValue(SessionKeys.InvestorType, investorType);
        }

        public static bool IdentifyAs(string source, string identifier)
        {
            if (Tracker.Current == null)
            {
                return false;
            }

            // Contact already has the identifier
            if (Tracker.Current.Session.Contact.Identifiers.Any(x => x.Source == source && x.Identifier == identifier))
            {
                return true;
            }

            var manager = Sitecore.Configuration.Factory.CreateObject("tracking/contactManager", true) as ContactManager;

            if (manager == null)
            {
                Log.Error("XConnectContactRepository: Unable to instantiate ContactManager", typeof(OnboardingHelper));
                return false;
            }

            var existingContact = manager.LoadContact(source, identifier);

            // No other contact has this identifier yet: just set it
            if (existingContact == null)
            {
                Tracker.Current.Session.IdentifyAs(source, identifier);
                var contactId = Tracker.Current.Contact.ContactId;

                Log.Info($"Add identifier for contact '{contactId}'. {source} > {identifier}", typeof(OnboardingHelper));

                manager.RemoveFromSession(contactId);
                Tracker.Current.Session.Contact = manager.LoadContact(contactId);
                return true;
            }

            // Other contact with identifier exists: Merge explicitly
            var currentContact = Tracker.Current.Session.Contact;
            var hasBehaviourProfiles = currentContact.BehaviorProfiles.Count != 0;

            Log.Info($"Merge contacts '{currentContact.ContactId}' into {existingContact.ContactId}. reason: {source} > {identifier}", typeof(OnboardingHelper));

            Tracker.Current.Session.Contact = manager.MergeContacts(existingContact, currentContact);

            manager.RemoveFromSession(Tracker.Current.Contact.ContactId);

            Tracker.Current.Session.Interaction.ContactId = Tracker.Current.Session.Contact.ContactId;
            Tracker.Current.Session.Interaction.ContactVisitIndex = Tracker.Current.Session.Contact.System.VisitCount;

            if (hasBehaviourProfiles || Tracker.Current.Interaction.Profiles.GetProfileNames().Length == 0)
            {
                InitializeInteractionProfilePipeline.Run(new InitializeInteractionProfileArgs(Tracker.Current.Session));
            }

            return true;
        }

        public static bool HasIdentifier(string source, string identifier)
        {
            if (Tracker.Current == null)
            {
                return false;
            }

            // Contact already has the identifier
            if (Tracker.Current.Session.Contact.Identifiers.Any(x => x.Source == source && x.Identifier == identifier))
            {
                return true;
            }

            return false;
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
                    Log.Error($"Xml for profile card is invalid. Value is {profileCard.ProfileCardValue}", ex, typeof(OnboardingHelper));
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
            if (!(query.HasKeys() && query.AllKeys.Contains(QueryStringNames.Change)))
            {
                query.Add(QueryStringNames.Change, bool.TrueString.ToLower());
            }
            
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri.PathAndQuery;
        }

        public static bool IsUkResident()
        {
            return GetCurrentContactCountryCode()?.Equals(CountryCodes.UK, StringComparison.InvariantCultureIgnoreCase) ?? false;
        }

        public static string GetCountryNameDefiniteArticle(ICountry country)
        {
            if (country == null)
            {
                return string.Empty;
            }

            var definiteArticle = string.Empty;
            if (country.UseDefiniteArticle)
            {
                definiteArticle = "the ";
            }

            return $"{definiteArticle}{country.CountryName}";
        }

        public static bool ShowMyLiontrust(IMvcContext context, BaseLog log, IEnumerable<IInvestor> allowedInvestors)
        {
            var currentInvestor = GetOnboardingInvestor(context, log);
            if (currentInvestor == null || !allowedInvestors.Any(i => i.Id.Equals(currentInvestor.Id)))
            {
                return false;
            }

            return true;
        }

        public static bool ShowLionHub(IMvcContext context, BaseLog log, IEnumerable<IInvestor> allowedInvestors, IEnumerable<IGlassBase> allowedPages)
        {
            var currentInvestor = GetOnboardingInvestor(context, log);
            if (currentInvestor == null || !allowedInvestors.Any(i => i.Id.Equals(currentInvestor.Id)))
            {
                return false;
            }

            var currentPage = context.GetContextItem<IGlassBase>();
            if (currentPage == null || !allowedPages.Any(p => p.Id.Equals(currentPage.Id)))
            {
                return false;
            }

            return true;
        }

        public static Profile ProfileCard(IOnboardingConfiguration onboardingConfiguration, BaseLog log)
        {
            var tracker = Tracker.Current;
            if (!IsValidConfiguration(onboardingConfiguration, log))
            {
                return null;
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
                            return profile;
                        }
                    }
                }
            }

            return null;
        }
    }
}