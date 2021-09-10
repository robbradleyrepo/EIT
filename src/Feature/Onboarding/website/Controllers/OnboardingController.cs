namespace LionTrust.Feature.Onboarding.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Onboarding.Models;
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Abstractions;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Annotations;
    using Sitecore.Mvc.Controllers;
    using Sitecore.XConnect.Client;
    using Sitecore.XConnect.Collection.Model;
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
        private ITracker _tracker;

        public OnboardingController(IMvcContext context, BaseLog log, ITrackerResolver resolver)
        {
            _context = context;
            this._log = log;
            _tracker = resolver.GetTracker();
        }

        public ActionResult Render(bool change = false)
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
            else if (!change && OnboardingComplete(data.OnboardingConfiguration))
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

        public ActionResult GetInvestorRoles(string countryIso)
        {
            var data = _context.GetHomeItem<IHome>();

            if (data == null || string.IsNullOrWhiteSpace(countryIso))
            {
                return null;
            }

            var chooseInvestor = data.OnboardingConfiguration?
                .ChooseInvestorRole?
                .FirstOrDefault();

            chooseInvestor.Investors = chooseInvestor?
                .Investors?
                .Where(i => !i.ExcludedCountires.Any(c => c.ISO == countryIso));

            if (chooseInvestor == null || chooseInvestor.Investors == null)
            {
                return null;
            }

            return View("~/Views/OnBoarding/ChooseInvestorRole.cshtml", chooseInvestor);
        }

        [HttpPost]
        public ActionResult Render([NotNull] OnboardingSubmit OnboardingSubmit)
        {
            if (ModelState.IsValid)
            {
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

                    var investor = data.OnboardingConfiguration.ChooseInvestorRole?
                        .FirstOrDefault()?
                        .Investors?
                        .FirstOrDefault(x => x.Id == OnboardingSubmit.InvestorId);

                    OnboardingHelper.AddPointsFromProfileCard(data.OnboardingConfiguration, investor.ProfileCard);

                    TrackAnonymousUser(OnboardingSubmit.Country);
                }
            }
            else
            {
                _log.Error("invalid data submitted", this);
            }

            var uriBuilder = new UriBuilder(Request.Url.ToString());
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Remove(Foundation.Onboarding.Constants.QueryStringNames.Change);
            uriBuilder.Query = query.ToString();

            return Redirect(uriBuilder.Uri.PathAndQuery);
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

            if (viewModel.ChooseInvestorRole.Investors == null)
            {
                _log.Error("Investors are null. Unable to render onboarding component", this);
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

            using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
            {
                var contact = OnboardingHelper.GetContact(client);
                var profile = OnboardingHelper.GetProfile(config.Profile.Name);

                if (!string.IsNullOrWhiteSpace(contact?.Addresses()?.PreferredAddress?.CountryCode)
                    && profile != null && profile.PatternId.HasValue
                    && config.ChooseInvestorRole.FirstOrDefault().Investors.Any(p => p.PatternCard.Id == profile.PatternId.Value))
                {
                    result = true;
                }
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

        public void TrackAnonymousUser(string country)
        {
            var manager = Sitecore.Configuration.Factory.CreateObject("tracking/contactManager", true) as Sitecore.Analytics.Tracking.ContactManager;

            if (manager != null)
            {
                Tracker.Current.Contact.ContactSaveMode = ContactSaveMode.AlwaysSave;
                manager.SaveContactToCollectionDb(Tracker.Current.Contact);

                using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                {
                    try
                    {
                        var contact = OnboardingHelper.GetContact(client);

                        if (contact != null)
                        {
                            var address = new Address
                            {
                                CountryCode = country
                            };

                            if (contact.Addresses() != null)
                            {
                                contact.Addresses().PreferredAddress = address;
                            }
                            else
                            {
                                client.SetAddresses(contact, new AddressList(address, AddressList.DefaultFacetKey));
                            }

                            client.Submit();
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error("Error saving data to profile", ex, this);
                    }
                }
            }
        }
    }
}