namespace LionTrust.Feature.Onboarding.Models
{
    using System.Linq;

    public class OnboardingViewModel
    {
        public OnboardingViewModel(IOnboardingConfiguration onboardingConfiguration)
        {
            Text = onboardingConfiguration.Text;
            Logo = onboardingConfiguration.Logo?.Src;
            ChooseCountry = onboardingConfiguration.ChooseCountry?.FirstOrDefault();
        }

        public string Text { get; private set; }

        public string Logo { get; private set; }

        public IChooseCountry ChooseCountry { get; set; }

    }
}