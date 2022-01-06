namespace LionTrust.Foundation.Onboarding.Models
{
    using System;
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Fields;
    using Glass.Mapper.Sc.Configuration.Attributes;     
    using Glass.Mapper.Sc.Configuration;

    using LionTrust.Foundation.ORM.Models;

    public interface IOnboardingConfiguration : IGlassBase
    {
        [SitecoreField(Constants.OnboardingConfiguration.Logo_FieldId)]
        Image Logo { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.Text_FieldId)]
        string Text { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.Profile_FieldId)]
        IGlassBase Profile { get; set; }

        [SitecoreField(Constants.OnboardingConfiguration.ProfessionalInvestor_FieldId)]
        IInvestor ProfressionalInvestor { get; set; }

        [SitecoreChildren(TemplateId = Constants.ChooseCountry.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<IChooseCountry> ChooseCountry { get; set; }

        [SitecoreChildren(TemplateId = Constants.ChooseInvestorRole.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<IChooseInvestorRole> ChooseInvestorRole { get; set; }

        [SitecoreChildren(TemplateId = Constants.TermsAndConditions.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<ITermsAndConditions> TermsAndConditions { get; set; }
    }
}