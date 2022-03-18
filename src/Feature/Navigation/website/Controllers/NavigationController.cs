﻿namespace LionTrust.Feature.Navigation.Controllers
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

    public class NavigationController : SitecoreController
    {
        private readonly INavigationRepository _navigationRepository;
        private readonly IMvcContext _mvcContext;
        private readonly BaseLog _log;
       
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
                    homeModel.YouAreViewingLabelWithArticle = OnboardingHelper.ViewingLabelWithArticle(homeModel.YouAreViewingLabel, homeModel.OnboardingRoleName);
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
                navigationViewModel.HomeItem.YouAreViewingLabelWithArticle = OnboardingHelper.ViewingLabelWithArticle(navigationViewModel.HomeItem.YouAreViewingLabel, navigationViewModel.HomeItem.OnboardingRoleName);
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
                    if (navigationViewModel.HomeItem.HeaderConfiguration != null && navigationViewModel.HomeItem.HeaderConfiguration.MenuItems != null)
                    {
                        navigationViewModel.MenuItems = navigationViewModel.HomeItem.HeaderConfiguration.MenuItems.Where(x => OnboardingHelper.HasAccess(x.Fund?.ExcludedCountries));
                    }                    
                }

                if (Sitecore.Context.Item.ID.Equals(homeItem.ID))
                {
                    navigationViewModel.Organization = _navigationRepository.GetOrganizationData(navigationViewModel.HomeItem, _mvcContext);
                }
            }            

            navigationViewModel.HomeItem.ChangeInvestorUrl = OnboardingHelper.GetChangeUrl();

            return navigationViewModel;
        }        
    }
}