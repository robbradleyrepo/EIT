namespace LionTrust.Feature.Onboarding.Rules.Conditions
{
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Foundation.Onboarding.Helpers;
    using Sitecore.DependencyInjection;
    using Sitecore.Rules;
    using Sitecore.Rules.Conditions;

    public class VisitorCountry<T> : WhenCondition<T> where T : RuleContext
    {
        public string Countries { get; set; }

        protected override bool Execute(T ruleContext)
        {
            var mvcContext = (IMvcContext)ServiceLocator.ServiceProvider.GetService(typeof(IMvcContext));
            var currentContactCountry = OnboardingHelper.GetCurrentContactCountry(mvcContext);

            return currentContactCountry != null && 
                (Countries ?? "").ToLower().Contains(currentContactCountry.Id.ToString().ToLower());
        }
    }
}