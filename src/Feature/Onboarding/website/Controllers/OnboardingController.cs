﻿namespace LionTrust.Feature.Onboarding.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Onboarding.Analytics;
    using LionTrust.Feature.Onboarding.Models;
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

            if (data?.OnboardingConfiguration == null
                || data.OnboardingConfiguration.Profile == null
                || data.OnboardingConfiguration.PrivateProfileCard == null
                || data.OnboardingConfiguration.ProfressionalProfileCard == null)
            {
                return null;
            }

            var viewModel = new OnboardingViewModel(data.OnboardingConfiguration);
            viewModel.ShowOnboarding = true;
            string isoCode;

            if (viewModel.ChooseCountry == null
                || viewModel.ChooseCountry.Regions == null
                || viewModel.ChooseCountry.Regions.Any(r => r.Countries == null)
                || viewModel.ChooseInvestorRole == null
                || viewModel.TermsAndConditions == null)
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
                    var regionInfo = new RegionInfo(OnboardingSubmit.Country);

                    if (regionInfo != null)
                    {
                        address.Country = regionInfo.EnglishName;
                    }
                }

                var data = _context.GetHomeItem<IHome>();

                if (data?.OnboardingConfiguration == null)
                {
                    return null;
                }
                else if (data.OnboardingConfiguration.Profile == null)
                {
                    _log.Error("Onboarding configuration profile has not been set", this);
                    return null;
                }
                else
                {
                    var profile = GetProfile(data.OnboardingConfiguration.Profile.Name, true);

                    if (profile != null)
                    {

                        if (OnboardingSubmit.Role == Roles.Private)
                        {
                            _cardManager.AddPointsFromProfileCard(data.OnboardingConfiguration.PrivateProfileCard, profile);
                        }
                        else if (OnboardingSubmit.Role == Roles.Profressional)
                        {
                            _cardManager.AddPointsFromProfileCard(data.OnboardingConfiguration.ProfressionalProfileCard, profile);
                        }
                    }
                }
            }

            return Render();
        }

        private bool OnboardingComplete(Feature.Onboarding.Models.IOnboardingConfiguration config)
        {
            var result = false;

            var profile = GetProfile(config.Profile.Name);

            if (profile != null && profile.PatternId.HasValue
                && new List<Guid>
                {
                    config.PrivateProfileCard.Id,
                    config.ProfressionalProfileCard.Id
                }.Contains(profile.PatternId.Value))
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