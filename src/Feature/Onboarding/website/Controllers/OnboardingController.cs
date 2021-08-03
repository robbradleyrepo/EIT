namespace LionTrust.Feature.Onboarding.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Onboarding.Analytics;
    using LionTrust.Feature.Onboarding.Models;
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Abstractions;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model;
    using Sitecore.Analytics.Model.Entities;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Annotations;
    using Sitecore.Mvc.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using static LionTrust.Feature.Onboarding.Constants;

    public class OnboardingController : SitecoreController
    {
        private IMvcContext _context;
        private readonly BaseLog _log;
        private readonly IProfileCardManager _cardManager;
        private ITracker _tracker;

        public OnboardingController(IMvcContext context, BaseLog log, ITrackerResolver resolver, IProfileCardManager cardManager)
        {
            _context = context;
            this._log = log;
            this._cardManager = cardManager;
            _tracker = resolver.GetTracker();
        }

        public ActionResult Render()
        {
            var data = _context.GetHomeItem<IHome>();

            if (!IsOnboardingConfigured(data))
            {
                return null;
            }

            var viewModel = new OnboardingViewModel(data.OnboardingConfiguration);
            viewModel.ShowOnboarding = true;
            string isoCode;

            if (!IsViewModelValid(viewModel))
            {
                return null;
            }
            else if (OnboardingComplete(data.OnboardingConfiguration))
            {
                viewModel.ShowOnboarding = false;
            }
            else if (_tracker != null && _tracker.Interaction != null
                && _tracker.Interaction.HasGeoIpData
                && !string.IsNullOrWhiteSpace(_tracker.Interaction.GeoData.Country))
            {
                isoCode = _tracker.Interaction.GeoData.Country;

                var cultureList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                var countryName = string.Empty;

                if (cultureList != null)
                {
                    var regionInfoList = cultureList.Select(x => new RegionInfo(x.TextInfo.CultureName));
                    if (regionInfoList != null)
                    {
                        countryName = regionInfoList.FirstOrDefault(r => r.TwoLetterISORegionName.Equals(isoCode, StringComparison.InvariantCultureIgnoreCase))?.EnglishName;
                    }
                }

                if (!string.IsNullOrWhiteSpace(countryName))
                {
                    viewModel.ChooseCountry.CurrentCountryName = countryName;
                    viewModel.ChooseCountry.CurrentCountryIso = isoCode;
                    SetTab(Tabs.CountryGeoIp);
                }
                else
                {
                    SetTab(Tabs.CountryList);
                }
            }
            else
            {
                SetTab(Tabs.CountryList);
            }

            return View("~/Views/OnBoarding/OnboardingOverlay.cshtml", viewModel);
        }

        public ActionResult GetTermsAndConditions(string countryIso)
        {
            var data = _context.GetHomeItem<IHome>();

            if (data == null || string.IsNullOrWhiteSpace(countryIso))
            {
                return null;
            }
            else
            {
                var termsAndConditions = data.OnboardingConfiguration.TermsAndConditions.FirstOrDefault();

                var country = data.OnboardingConfiguration?
                    .ChooseCountry?
                    .SelectMany(x => x.Regions?
                    .SelectMany(r => r.Countries))?
                    .FirstOrDefault(c => c.ISO == countryIso);

                if (termsAndConditions == null || country == null)
                {
                    return null;
                }
                else
                {
                    return View("~/Views/OnBoarding/TermsText.cshtml", country);
                }
            }
        }

        [HttpPost]
        public ActionResult Render([NotNull] OnboardingSubmit OnboardingSubmit)
        {
            if (ModelState.IsValid)
            {
                var address = GetAddress(true);
                if (address != null)
                {
                    try
                    {
                        var regionInfo = new RegionInfo(OnboardingSubmit.Country);

                        if (regionInfo != null)
                        {
                            address.Country = regionInfo.EnglishName;
                        }
                    }
                    catch (ArgumentException)
                    {
                        string message = $"{OnboardingSubmit.Country} is not an valid value";
                        _log.Info(message, this);
                    }
                }

                var data = _context.GetHomeItem<IHome>();

                if (data?.OnboardingConfiguration == null)
                {
                    _log.Error("Onboarding configuration has not been set", this);
                }
                else if (data.OnboardingConfiguration.Profile == null)
                {
                    _log.Error("Onboarding configuration profile has not been set", this);
                }
                else
                {
                    var profile = GetProfile(data.OnboardingConfiguration.Profile.Name, true);

                    if (profile != null)
                    {

                        if (OnboardingSubmit.InvestorType == Foundation.Onboarding.Constants.InvestorType.Private)
                        {
                            _cardManager.AddPointsFromProfileCard(data.OnboardingConfiguration.PrivateProfileCard, profile);
                        }
                        else if (OnboardingSubmit.InvestorType == Foundation.Onboarding.Constants.InvestorType.Professional)
                        {
                            _cardManager.AddPointsFromProfileCard(data.OnboardingConfiguration.ProfressionalProfileCard, profile);
                        }

                    }
                    else
                    {
                        _log.Error("Profile not found", this);
                    }
                }
            }
            else
            {
                _log.Error("invalid data submitted", this);
            }

            return Redirect(Request.Url.ToString());
        }

        private bool IsOnboardingConfigured(IHome data)
        {
            if (data == null)
            {
                _log.Error("Home item is not found", this);
                return false;
            }

            if (data.OnboardingConfiguration == null)
            {
                _log.Error("Onboarding configuration has not been set", this);
                return false;
            }

            if (data.OnboardingConfiguration.Profile == null)
            {
                _log.Error("Onboarding configuration profile has not been set", this);
                return false;
            }

            if (data.OnboardingConfiguration.PrivateProfileCard == null)
            {
                _log.Error("Onboarding configuration private profile card has not been set", this);
                return false;
            }

            if (data.OnboardingConfiguration.ProfressionalProfileCard == null)
            {
                _log.Error("Onboarding configuration professional profile card has not been set", this);
                return false;
            }

            return true;
        }

        private bool IsViewModelValid(OnboardingViewModel viewModel)
        {
            if (viewModel == null)
            {
                _log.Error("View model is null so unable to render onboarding component", this);
                return false;
            }

            if (viewModel.ChooseCountry == null)
            {
                _log.Error("View model choose country is null so unable to render onboarding component", this);
                return false;
            }

            if (viewModel.ChooseCountry.Regions == null)
            {
                _log.Error("Choose country regions are null so unable to render onboarding component", this);
                return false;
            }

            if (viewModel.ChooseCountry.Regions.Any(r => r.Countries == null))
            {
                _log.Error("One region has a country that is set to null. Unable to render onboarding component", this);
                return false;
            }

            if (viewModel.ChooseInvestorRole == null)
            {
                _log.Error("Choose investor role is null. Unable to render onboarding component", this);
                return false;
            }

            if (viewModel.TermsAndConditions == null)
            {
                _log.Error("Terms and conditions are null. Unable to render onboarding component", this);
                return false;
            }

            return true;
        }

        private bool OnboardingComplete(IOnboardingConfiguration config)
        {
            var result = false;

            var profile = GetProfile(config.Profile.Name);

            if (profile != null && profile.PatternId.HasValue)
            {
                result = true;
            }

            return result;
        }

        private void SetTab(Tabs tab)
        {
            var currentTab = Request.Cookies.Get(Cookies.CurrentTab_CookieName);
            var index = (int)tab;

            if (currentTab == null)
            {
                currentTab = new HttpCookie(Cookies.CurrentTab_CookieName, index.ToString());
            }
            else
            {
                currentTab.Value = index.ToString();
            }

            Response.SetCookie(currentTab);
        }

        private IAddress GetAddress(bool createIfNull = false)
        {
            IAddress address = null;

            if (_tracker != null && _tracker.Contact != null)
            {
                var contact = _tracker.Contact;
                var addresses = contact.GetFacet<IContactAddresses>(Onboarding.Constants.Analytics.Addresses_FacetName);

                if (addresses != null && addresses.Entries.Contains(Onboarding.Constants.Analytics.DefaultAddress_EntityName))
                {
                    address = addresses.Entries[Onboarding.Constants.Analytics.DefaultAddress_EntityName];
                }
                else if (addresses != null && createIfNull)
                {
                    address = addresses.Entries.Create(Onboarding.Constants.Analytics.DefaultAddress_EntityName);
                }
            }

            return address;
        }

        private Profile GetProfile(string profileName, bool createNew = false)
        {
            Profile profile = null;

            if (_tracker != null && _tracker.Interaction != null && _tracker.Interaction.Profiles != null)
            {

                if (_tracker.Interaction.Profiles.ContainsProfile(profileName))
                {
                    if (createNew)
                    {
                        _tracker.Interaction.Profiles.Remove(profileName);
                    }
                    else
                    {
                        profile = _tracker.Interaction.Profiles[profileName];
                    }
                }

                if (profile == null && createNew)
                {
                    var listOfProfileData = new List<ProfileData>()
                    {
                        new ProfileData(profileName)
                    };

                    _tracker.Interaction.Profiles.Initialize(listOfProfileData);
                    profile = _tracker.Interaction.Profiles[profileName];
                }
            }

            return profile;

        }        
    }
}