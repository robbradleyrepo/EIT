namespace LionTrust.Feature.Onboarding.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IHome : IGlassBase
    {
        [SitecoreField(Constants.Home.OnboardingConfiguation_FieldId)]
        IOnboardingConfiguration OnboardingConfiguration { get; set; }
    }
}
