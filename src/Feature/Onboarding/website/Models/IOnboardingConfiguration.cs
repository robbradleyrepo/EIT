namespace LionTrust.Feature.Onboarding.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    public interface IOnboardingConfiguration : IGlassBase
    {
        [SitecoreField(Constants.OnboardingConfiguration.Logo_FieldId)]
        Image Logo { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.Text_FieldId)]
        string Text { get; set; }

        [SitecoreChildren(TemplateId = Constants.ChooseCountry.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<IChooseCountry> ChooseCountry { get; set; }

        [SitecoreChildren(TemplateId = Constants.ChooseInvestorRole.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<IChooseInvestorRole> ChooseInvestorRole { get; set; }

        [SitecoreChildren(TemplateId = Constants.TermsAndConditions.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<ITermsAndConditions> TermsAndConditions { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.Profile_FieldId)]
        IGlassBase Profile { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.PrivateProfileCard_FieldId)]
        IProfileCard PrivateProfileCard { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.ProfessionalProfileCard_FieldId)]
        IProfileCard ProfressionalProfileCard { get; set; }
    }


}