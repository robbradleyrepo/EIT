namespace LionTrust.Foundation.Navigation.Helpers
{
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Abstractions;
    using static LionTrust.Foundation.Onboarding.Constants;

    public class NavigationHelper
    {
        public static IHeaderConfiguration GetCurrentHeaderConfiguration(IOnboardingConfiguration configuration, BaseLog log)
        {
            var investorType = OnboardingHelper.GetInvestorType(configuration, log);
            switch (investorType)
            {
                case InvestorType.Private:
                    {
                        return configuration.PrivateHeaderConfiguration;
                    }
                case InvestorType.Professional:
                    {
                        return configuration.ProfessionalHeaderConfiguration;
                    }
                case InvestorType.Analyst:
                    {
                        return configuration.AnalystHeaderConfiguration;
                    }
                case InvestorType.Journalyst:
                    {
                        return configuration.JournalistHeaderConfiguration;
                    }
                default:
                    {
                        return configuration.ProfessionalHeaderConfiguration;
                    }
            }
        }
    }
}