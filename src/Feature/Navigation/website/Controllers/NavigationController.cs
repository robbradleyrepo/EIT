namespace LionTrust.Feature.Navigation.Controllers
{
    using System.Web.Mvc;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Navigation.Models;
    using LionTrust.Feature.Navigation.Repositories;
    using LionTrust.Foundation.Onboarding.Helpers;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
    using Sitecore.Mvc.Presentation;
    using System.Linq;
    using LionTrust.Foundation.Navigation.Helpers;
    using LionTrust.Foundation.Schema.Models;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;
    using System.Collections.Generic;
    using System;

    public class NavigationController : SitecoreController
    {
        private readonly INavigationRepository _navigationRepository;
        private readonly IMvcContext _mvcContext;
        private readonly BaseLog _log;

        public const int LOGO_WIDTH = 166;
        public const int LOGO_HEIGHT = 52;

        public NavigationController(INavigationRepository navigationRepository, IMvcContext mvcContext, BaseLog log)
        {
            this._navigationRepository = navigationRepository;
            this._mvcContext = mvcContext;
            this._log = log;
        }

        public ActionResult Header()
        {
            NavigationViewModel navigationViewModel = GetNavigationViewModel();

            return View("~/Views/Navigation/Header.cshtml", navigationViewModel);
        }

        public ActionResult Footer()
        {
            var item = RenderingContext.Current.Rendering.Item;
            var homeItem = _navigationRepository.GetNavigationRoot(item);
            IHome homeModel = null;
            if (homeItem != null)
            {
                homeModel = _mvcContext.SitecoreService.GetItem<IHome>(homeItem.ID.Guid);
                if (homeModel.OnboardingConfiguration != null)
                {
                    homeModel.OnboardingRoleName = OnboardingHelper.ProfileRoleName(homeModel.OnboardingConfiguration, _log);
                }

                homeModel.ChangeInvestorUrl = OnboardingHelper.GetChangeUrl();
            }

            return View("~/Views/Navigation/Footer.cshtml", homeModel);
        }

        public ActionResult Menu()
        {
            NavigationViewModel navigationViewModel = GetNavigationViewModel();
            if (navigationViewModel.HomeItem != null && navigationViewModel.HomeItem.OnboardingConfiguration != null)
            {
                navigationViewModel.HomeItem.OnboardingRoleName = OnboardingHelper.ProfileRoleName(navigationViewModel.HomeItem.OnboardingConfiguration, _log);
            }

            return View("~/Views/Navigation/Menu.cshtml", navigationViewModel);
        }

        private NavigationViewModel GetNavigationViewModel()
        {
            var navigationViewModel = new NavigationViewModel();
            var item = RenderingContext.Current.Rendering.Item;            
           
            var homeItem = _navigationRepository.GetNavigationRoot(item);
            if (homeItem != null)
            {
                navigationViewModel.HomeItem = _mvcContext.SitecoreService.GetItem<IHome>(homeItem.ID.Guid);                
                if (navigationViewModel.HomeItem.OnboardingConfiguration != null) 
                {
                    navigationViewModel.HomeItem.HeaderConfiguration = NavigationHelper.GetCurrentHeaderConfiguration(_mvcContext, navigationViewModel.HomeItem.OnboardingConfiguration, _log);
                }

                if (Sitecore.Context.Item.ID.Equals(homeItem.ID))
                {
                    navigationViewModel.Organization = GetOrganizationData(navigationViewModel.HomeItem);
                }
            }

            navigationViewModel.HomeItem.MenuItems = navigationViewModel.HomeItem.MenuItems.Where(x => OnboardingHelper.HasAccess(x.Fund?.ExcludedCountries));

            navigationViewModel.HomeItem.ChangeInvestorUrl = OnboardingHelper.GetChangeUrl();

            return navigationViewModel;
        }

        private OrganizationSchema GetOrganizationData(IHome home)
        {
            var organizationSchema = new OrganizationSchema();

            if (home != null)
            {
                organizationSchema.Url = home.AbsoluteUrl;
                organizationSchema.Description = home.PageShortDescription;
                organizationSchema.Name = home.CompanyName;
                organizationSchema.ContactType = home.ContactType;
                organizationSchema.AreaServed = home.AreaServed;

                if (home.Logo != null)
                {
                    var imageItem = _mvcContext.SitecoreService.GetItem<Item>(home.Logo.MediaId);
                    if (imageItem != null)
                    {
                        var mediaOption = new MediaUrlOptions()
                        {
                            AlwaysIncludeServerUrl = true,
                            AbsolutePath = true,
                            LowercaseUrls = true,
                            RequestExtension = ""                            
                        };
                        organizationSchema.LogoUrl = MediaManager.GetMediaUrl(imageItem, mediaOption);
                        organizationSchema.LogoHeight = home.Logo.Height != 0 ? home.Logo.Height : LOGO_HEIGHT;
                        organizationSchema.LogoWidth = home.Logo.Width != 0 ? home.Logo.Width : LOGO_WIDTH;
                    }
                }

                var footerConfig = home.FooterConfiguration;
                if (footerConfig != null)
                {
                    organizationSchema.Telephone = footerConfig.PhoneNumber;
                    organizationSchema.Email = footerConfig.Email;
                    organizationSchema.Address = footerConfig.Address;
                    organizationSchema.Location = footerConfig.Location;
                    organizationSchema.PostalCode = footerConfig.PostalCode;
                    organizationSchema.Longitude = footerConfig.Longitude;
                    organizationSchema.Latitude = footerConfig.Latitude;

                    if (footerConfig.SameAs != null)
                    {
                        var linkList = new List<Uri>();
                        foreach (var socialLink in footerConfig.SameAs)
                        {
                            if (socialLink.SocialLink != null && !string.IsNullOrEmpty(socialLink.SocialLink.Url))
                            {
                                linkList.Add(new Uri(socialLink.SocialLink.Url));
                            }
                        }

                        organizationSchema.SameAs = linkList;
                    }
                }
            }

            return organizationSchema;
        }
    }
}