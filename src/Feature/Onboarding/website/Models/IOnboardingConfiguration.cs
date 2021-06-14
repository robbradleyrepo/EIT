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
    }
}