namespace LionTrust.Foundation.Onboarding.Models
{
    using Glass.Mapper.Sc.Fields;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;

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

        [SitecoreField(Constants.OnboardingConfiguration.PrivateHeaderConfiguration_FieldId)]
        IHeaderConfiguration PrivateHeaderConfiguration { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.ProfessionalProfileCard_FieldId)]
        IProfileCard ProfressionalProfileCard { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.PrivatePatternCard_FieldId)]
        IGlassBase PrivatePatternCard { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.ProfessionalPatternCard_FieldId)]
        IGlassBase ProfressionalPatternCard { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.ProfessionalHeaderConfiguration_FieldId)]
        IHeaderConfiguration ProfessionalHeaderConfiguration { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.JournalistHeaderConfigurationÜFieldId)]
        IHeaderConfiguration JournalistHeaderConfiguration { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.AnalystHeaderConfigurationÜFieldId)]
        IHeaderConfiguration AnalystHeaderConfiguration { get; set; }

        [SitecoreChildren(TemplateId = Constants.ChooseCountry.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<IChooseCountry> ChooseCountry { get; set; }

        [SitecoreChildren(TemplateId = Constants.ChooseInvestorRole.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<IChooseInvestorRole> ChooseInvestorRole { get; set; }

        [SitecoreChildren(TemplateId = Constants.TermsAndConditions.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<ITermsAndConditions> TermsAndConditions { get; set; }
    }
}