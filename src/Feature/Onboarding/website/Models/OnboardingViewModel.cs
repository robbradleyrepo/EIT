namespace LionTrust.Feature.Onboarding.Models
{
    using LionTrust.Foundation.Onboarding.Models;
    using System.Linq;

    public class OnboardingViewModel
    {
        public OnboardingViewModel(IOnboardingConfiguration onboardingConfiguration)
        {
            Text = onboardingConfiguration.Text;
            Logo = onboardingConfiguration.Logo?.Src;
            ChooseCountry = onboardingConfiguration.ChooseCountry?.FirstOrDefault();
            ChooseInvestorRole = onboardingConfiguration.ChooseInvestorRole?.FirstOrDefault();
            TermsAndConditions = onboardingConfiguration.TermsAndConditions?.FirstOrDefault();
        }

        public string Text { get; private set; }

        public string Logo { get; private set; }

        public IChooseCountry ChooseCountry { get; private set; }

        public IChooseInvestorRole ChooseInvestorRole { get; private set; }

        public ITermsAndConditions TermsAndConditions { get; private set; }

        public bool ShowOnboarding { get; set; }

    }
}