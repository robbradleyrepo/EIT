namespace LionTrust.Foundation.Onboarding.Models
{
    using Glass.Mapper.Sc.Fields;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface IOnboardingConfiguration : IGlassBase
    {
        [SitecoreField(Constants.OnboardingConfiguration.Logo_FieldId)]
        Image Logo { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.Text_FieldId)]
        string Text { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.Profile_FieldId)]
        IGlassBase Profile { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.PrivateProfileCard_FieldId)]
        IProfileCard PrivateProfileCard { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.ProfessionalProfileCard_FieldId)]
        IProfileCard ProfressionalProfileCard { get; set; }
    }
}