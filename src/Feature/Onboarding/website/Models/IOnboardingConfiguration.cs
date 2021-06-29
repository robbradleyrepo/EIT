namespace LionTrust.Feature.Onboarding.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    public interface IOnboardingConfiguration : Foundation.Onboarding.Models.IOnboardingConfiguration, IGlassBase
    {   
        [SitecoreChildren(TemplateId = Constants.ChooseCountry.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<IChooseCountry> ChooseCountry { get; set; }

        [SitecoreChildren(TemplateId = Constants.ChooseInvestorRole.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<IChooseInvestorRole> ChooseInvestorRole { get; set; }

        [SitecoreChildren(TemplateId = Constants.TermsAndConditions.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<ITermsAndConditions> TermsAndConditions { get; set; }
    }
}