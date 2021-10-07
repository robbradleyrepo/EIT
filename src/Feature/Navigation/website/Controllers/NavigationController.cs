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
            var rootItem = _navigationRepository.GetNavigationSiteRoot(item);
            if (rootItem != null)
            {
                navigationViewModel.RootItem = _mvcContext.SitecoreService.GetItem<ISiteRoot>(rootItem.ID.Guid);
            }

            var homeItem = _navigationRepository.GetNavigationRoot(item);
            if (homeItem != null)
            {
                navigationViewModel.HomeItem = _mvcContext.SitecoreService.GetItem<IHome>(homeItem.ID.Guid);
                if (navigationViewModel.HomeItem.OnboardingConfiguration != null) 
                {
                    navigationViewModel.HomeItem.HeaderConfiguration = NavigationHelper.GetCurrentHeaderConfiguration(_mvcContext, navigationViewModel.HomeItem.OnboardingConfiguration, _log);
                }
            }

            navigationViewModel.HomeItem.MenuItems = navigationViewModel.HomeItem.MenuItems.Where(x => OnboardingHelper.HasAccess(x.Fund?.ExcludedCountries));

            navigationViewModel.HomeItem.ChangeInvestorUrl = OnboardingHelper.GetChangeUrl();

            return navigationViewModel;
        }
    }
}