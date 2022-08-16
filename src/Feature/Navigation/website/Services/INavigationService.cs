using LionTrust.Feature.Navigation.Models;
using LionTrust.Foundation.Onboarding.Models;

namespace LionTrust.Feature.Navigation.Services
{
    public interface INavigationService
    {
        NavigationViewModel GetNavigationViewModel();

        string GetOnboardingRoleName(IOnboardingConfiguration onboardingConfiguration);
    }
}
