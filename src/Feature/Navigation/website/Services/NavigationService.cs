using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.Navigation.Models;
using LionTrust.Feature.Navigation.Repositories;
using LionTrust.Foundation.Navigation.Helpers;
using LionTrust.Foundation.Onboarding.Helpers;
using LionTrust.Foundation.ORM.Models;
using Sitecore.Abstractions;
using Sitecore.Mvc.Presentation;
using System.Linq;

namespace LionTrust.Feature.Navigation.Services
{
    public class NavigationService : INavigationService
    {
        private readonly INavigationRepository _navigationRepository;
        private readonly IMvcContext _mvcContext;
        private readonly BaseLog _log;

        public NavigationService(INavigationRepository navigationRepository, IMvcContext mvcContext, BaseLog log)
        {
            _navigationRepository = navigationRepository;
            _mvcContext = mvcContext;
            _log = log;
        }

        public NavigationViewModel GetNavigationViewModel()
        {
            var navigationViewModel = new NavigationViewModel();
            var item = RenderingContext.Current.Rendering.Item;

            var homeItem = _navigationRepository.GetNavigationRoot(item);
            if (homeItem != null)
            {
                navigationViewModel.HomeItem = _mvcContext.SitecoreService.GetItem<IHome>(homeItem.ID.Guid);

                if (navigationViewModel.HomeItem != null)
                {
                    var country = OnboardingHelper.GetCurrentContactCountry(_mvcContext);
                    navigationViewModel.HomeItem.CurrentCountry = OnboardingHelper.GetCountryNameDefiniteArticle(country);
                    if (navigationViewModel.HomeItem.OnboardingConfiguration != null)
                    {
                        navigationViewModel.HomeItem.HeaderConfiguration = NavigationHelper.GetCurrentHeaderConfiguration(_mvcContext, navigationViewModel.HomeItem.OnboardingConfiguration, _log);
                        if (navigationViewModel.HomeItem.HeaderConfiguration != null && navigationViewModel.HomeItem.HeaderConfiguration.MenuItems != null)
                        {
                            navigationViewModel.MenuItems = navigationViewModel.HomeItem.HeaderConfiguration.MenuItems.Where(x => OnboardingHelper.HasAccess(x.Fund?.ExcludedCountries));
                        }
                    }

                    navigationViewModel.HomeItem.ChangeInvestorUrl = OnboardingHelper.GetChangeUrl();

                    if (navigationViewModel.HomeItem.MyLiontrusAllowedInvestors != null && navigationViewModel.HomeItem.MyLiontrusAllowedInvestors.Any())
                    {
                        navigationViewModel.ShowMyLiontrust = OnboardingHelper.ShowMyLiontrust(_mvcContext, _log, navigationViewModel.HomeItem.MyLiontrusAllowedInvestors);
                    }

                    if (navigationViewModel.HomeItem.LionHubAllowedInvestors != null && navigationViewModel.HomeItem.LionHubAllowedInvestors.Any() &&
                        navigationViewModel.HomeItem.LionHubAllowedPages != null && navigationViewModel.HomeItem.LionHubAllowedPages.Any())
                    {
                        navigationViewModel.ShowLionHub = OnboardingHelper.ShowLionHub(_mvcContext, _log, navigationViewModel.HomeItem.LionHubAllowedInvestors, navigationViewModel.HomeItem.LionHubAllowedPages);
                    }
                }

                if (Sitecore.Context.Item.ID.Equals(homeItem.ID))
                {
                    navigationViewModel.Organization = _navigationRepository.GetOrganizationData(navigationViewModel.HomeItem, _mvcContext);
                }
            }

            return navigationViewModel;
        }

        public string GetOnboardingRoleName(Foundation.Onboarding.Models.IOnboardingConfiguration onboardingConfiguration)
        {
            var investorType = string.Empty;
            var investorTypeId = OnboardingHelper.GetInvestorTypeId();

            if (investorTypeId.HasValue)
            {
                var patternCard = _mvcContext.SitecoreService.GetItem<IGlassBase>(investorTypeId.Value);
                investorType = patternCard.Name;
            }

            if (string.IsNullOrEmpty(investorType))
            {
                investorType = OnboardingHelper.ProfileRoleName(onboardingConfiguration, _log);
            }

            return investorType;
        }
    }
}