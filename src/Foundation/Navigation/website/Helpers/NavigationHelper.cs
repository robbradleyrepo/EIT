namespace LionTrust.Foundation.Navigation.Helpers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Foundation.Navigation.Models;
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Onboarding.Models;
    using Sitecore.Abstractions;
    using static LionTrust.Foundation.Onboarding.Constants;

    public class NavigationHelper
    {
        public static IHeaderConfiguration GetCurrentHeaderConfiguration(IMvcContext mvcContext, IOnboardingConfiguration configuration, BaseLog log)
        {
            var investorType = OnboardingHelper.GetInvestorType(configuration, log);
            switch (investorType)
            {
                case InvestorType.Private:
                    {
                        return mvcContext.SitecoreService.GetItem<IHeaderConfiguration>(configuration.PrivateHeaderConfiguration);
                    }
                case InvestorType.Professional:
                    {
                        return mvcContext.SitecoreService.GetItem<IHeaderConfiguration>(configuration.ProfessionalHeaderConfiguration);
                    }
                case InvestorType.Analyst:
                    {
                        return mvcContext.SitecoreService.GetItem<IHeaderConfiguration>(configuration.AnalystHeaderConfiguration);
                    }
                case InvestorType.Journalyst:
                    {
                        return mvcContext.SitecoreService.GetItem<IHeaderConfiguration>(configuration.JournalistHeaderConfiguration);
                    }
                default:
                    {
                        return mvcContext.SitecoreService.GetItem<IHeaderConfiguration>(configuration.ProfessionalHeaderConfiguration);
                    }
            }
        }
    }
}