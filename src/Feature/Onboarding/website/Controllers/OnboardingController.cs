namespace LionTrust.Feature.Onboarding.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Onboarding.Models;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model.Entities;
    using Sitecore.Mvc.Controllers;
    using System.Web;
    using System.Web.Mvc;

    public class OnboardingController : SitecoreController
    {
        private IMvcContext _context;
        private ITracker _tracker;
        public OnboardingController(IMvcContext context)
        {
            _context = context;
            _tracker = Tracker.Current;
        }

        public ActionResult Render()
        {
            var data = _context.GetHomeItem<IHome>();

            if (data?.OnboardingConfiguration == null)
            {
                return null;
            }

            var viewModel = new OnboardingViewModel(data.OnboardingConfiguration);

            if (viewModel.ChooseCountry == null)
            {
                return null;
            }
            else if (HasCountry())
            {
                SetTab(2);
            }
            else if (_tracker != null && _tracker.Interaction != null
                && _tracker.Interaction.HasGeoIpData
                && !string.IsNullOrWhiteSpace(_tracker.Interaction.GeoData.Country))
            {
                viewModel.ChooseCountry.CurrentCountry = _tracker.Interaction.GeoData.Country;
                SetTab(1);
            }
            else
            {
                SetTab(0);
            }

            return View("~/Views/OnBoarding/OnboardingOverlay.cshtml", viewModel);
        }

        private bool HasCountry()
        {
            var result = false;

            var address = GetAddress();

            if (address != null && !string.IsNullOrWhiteSpace(address.Country))
            {
                result = true;
            }

            return result;
        }

        private void SetTab(int index)
        {
            var currentTab = Request.Cookies.Get(Onboarding.Constants.Cookies.CurrentTab_CookieName);

            if (currentTab == null)
            {
                currentTab = new HttpCookie(Onboarding.Constants.Cookies.CurrentTab_CookieName, index.ToString());
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
                else if(addresses != null && createIfNull)
                {
                    address = addresses.Entries.Create(Onboarding.Constants.Analytics.DefaultAddress_EntityName);
                }
            }

            return address;
        }
    }
}