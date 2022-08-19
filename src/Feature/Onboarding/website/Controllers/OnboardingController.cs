namespace LionTrust.Feature.Onboarding.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Onboarding.Models;
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Abstractions;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model;
    using Sitecore.Annotations;
    using Sitecore.Mvc.Controllers;
    using Sitecore.XConnect.Client;
    using Sitecore.XConnect.Collection.Model;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using static LionTrust.Feature.Onboarding.Constants;
    using static LionTrust.Foundation.Onboarding.Constants;

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
                    var country = OnboardingHelper.GetCountryFromIso(_context, isoCode);                       
                    viewModel.ChooseCountry.CurrentCountryName = OnboardingHelper.GetCountryNameDefiniteArticle(country);
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

        public ActionResult GetTermsAndConditions(string countryIso, string investorType)
        {
            var data = _context.GetHomeItem<IHome>();

            if (data == null || string.IsNullOrWhiteSpace(countryIso) || string.IsNullOrWhiteSpace(investorType))
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
                    var termsText = string.Empty;
                    if (Guid.TryParse(investorType, out Guid investorTypeId))
                    {
                        var investor = data.OnboardingConfiguration.ChooseInvestorRole?
                            .FirstOrDefault()?
                            .Investors?
                            .FirstOrDefault(x => x.Id == investorTypeId);

                        if (investor == null)
                        {
                            return null;
                        }

                        switch (investor.Name)
                        {
                            case InvestorTypes.ProfessionalInvestor:
                                termsText = country.TermsAndConditionsProfessional;
                                break;
                            case InvestorTypes.PrivateInvestor:
                                termsText = country.TermsAndConditionsPrivate;
                                break;
                            case InvestorTypes.InstitutionalInvestor:
                                termsText = country.TermsAndConditionsInstitutional;
                                break;
                            case InvestorTypes.Journalist:
                                termsText = country.TermsAndConditionsJournalist;
                                break;
                            case InvestorTypes.Shareholder:
                                termsText = country.TermsAndConditionsShareholder;
                                break;
                            case InvestorTypes.Charity:
                                termsText = country.TermsAndConditionsCharity;
                                break;
                        }
                    }
                    
                    if (string.IsNullOrEmpty(termsText))
                    {
                        termsText = country.TermsAndConditionsProfessional;
                    }

                    return View("~/Views/OnBoarding/TermsText.cshtml", new TermsAndConditionsViewModel { Text = termsText } );
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
            if (!ModelState.IsValid)
            {
                return new EmptyResult();
            }

            if (_context?.ContextItem?.ID == NotFoundPage.ItemId)
            {
                return null;
            }

            var data = _context.GetHomeItem<IHome>();
            var landingPageUrl = string.Empty;

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

                IProfileCard profileCard;                
                if (OnboardingSubmit.Country.Equals(CountryCodes.UK))
                {
                    profileCard = investor.ProfileCard;
                }
                else
                {
                    profileCard = investor.InternationalProfileCard;
                }

                OnboardingHelper.AddPointsFromProfileCard(data.OnboardingConfiguration, profileCard);
                OnboardingHelper.UpdateInvestorTypeSession(investor.PatternCard.Id);

                TrackAnonymousUser(OnboardingSubmit.Country);

                landingPageUrl = investor.LandingPage?.Url;
            }

            if (!string.IsNullOrEmpty(landingPageUrl) && (data?.AbsoluteUrl == Request?.Url?.AbsoluteUri))
            {
                return Redirect(landingPageUrl);
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
                var investors = config.ChooseInvestorRole?.FirstOrDefault()?.Investors;
                if (investors == null)
                {
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(contact?.Addresses()?.PreferredAddress?.CountryCode)
                    && profile != null && profile.PatternId.HasValue
                    && IsInvestorSelected(investors, profile.PatternId.Value))
                {
                    result = true;
                }
            }

            return result;
        }

        private bool IsInvestorSelected(IEnumerable<IInvestor> investors, Guid patternId)
        {
            var isUkResident = OnboardingHelper.IsUkResident();            

            if (isUkResident && investors.Any(p => p.PatternCard.Id == patternId) ||
               !isUkResident && investors.Any(p => p.InternationalPatternCard.Id == patternId))
            {
                return true;
            }

            return false;
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
                            OnboardingHelper.UpdateContactSession(contact);
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