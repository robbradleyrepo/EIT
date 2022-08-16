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
    using LionTrust.Feature.Navigation.Services;

    public class NavigationController : SitecoreController
    {
        private readonly INavigationService _navigationService;
        private readonly INavigationRepository _navigationRepository;
        private readonly IMvcContext _mvcContext;
       
        public NavigationController(INavigationService navigationService, INavigationRepository navigationRepository, IMvcContext mvcContext)
        {
            _navigationService = navigationService;
            _navigationRepository = navigationRepository;
            _mvcContext = mvcContext;
        }

        public ActionResult Header()
        {
            NavigationViewModel navigationViewModel = _navigationService.GetNavigationViewModel();

            return View("~/Views/Navigation/Header.cshtml", navigationViewModel);
        }

        public ActionResult LoginHeader()
        {
            NavigationViewModel navigationViewModel = _navigationService.GetNavigationViewModel();

            if (navigationViewModel.HomeItem != null && navigationViewModel.HomeItem.OnboardingConfiguration != null)
            {
                navigationViewModel.HomeItem.OnboardingRoleName = _navigationService.GetOnboardingRoleName(navigationViewModel.HomeItem.OnboardingConfiguration);
                navigationViewModel.HomeItem.YouAreViewingLabelWithArticle = OnboardingHelper.ViewingLabelWithArticle(navigationViewModel.HomeItem.YouAreViewingLabel, navigationViewModel.HomeItem.OnboardingRoleName);
            }

            return View("~/Views/Navigation/LoginHeader.cshtml", navigationViewModel);
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
                    homeModel.OnboardingRoleName = _navigationService.GetOnboardingRoleName(homeModel.OnboardingConfiguration);
                    homeModel.YouAreViewingLabelWithArticle = OnboardingHelper.ViewingLabelWithArticle(homeModel.YouAreViewingLabel, homeModel.OnboardingRoleName);
                    var country = OnboardingHelper.GetCurrentContactCountry(_mvcContext);
                    homeModel.CurrentCountry = OnboardingHelper.GetCountryNameDefiniteArticle(country);
                }

                homeModel.ChangeInvestorUrl = OnboardingHelper.GetChangeUrl();
            }

            return View("~/Views/Navigation/Footer.cshtml", homeModel);
        }

        public ActionResult Menu()
        {
            NavigationViewModel navigationViewModel = _navigationService.GetNavigationViewModel();
            if (navigationViewModel.HomeItem != null && navigationViewModel.HomeItem.OnboardingConfiguration != null)
            {
                navigationViewModel.HomeItem.OnboardingRoleName = _navigationService.GetOnboardingRoleName(navigationViewModel.HomeItem.OnboardingConfiguration);
                navigationViewModel.HomeItem.YouAreViewingLabelWithArticle = OnboardingHelper.ViewingLabelWithArticle(navigationViewModel.HomeItem.YouAreViewingLabel, navigationViewModel.HomeItem.OnboardingRoleName);
            }            

            return View("~/Views/Navigation/Menu.cshtml", navigationViewModel);
        }
    }
}