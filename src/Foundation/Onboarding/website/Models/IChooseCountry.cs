namespace LionTrust.Foundation.Onboarding.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = Constants.ChooseCountry.TemplateId)]
    public interface IChooseCountry : IGlassBase
    {
        [SitecoreField(Constants.ChooseCountry.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.ChooseCountry.Subtitle_FieldId)]
        string Subtitle { get; set; }

        [SitecoreField(Constants.ChooseCountry.CorrectLocationText_FieldId)]
        string CorrectLocationText { get; set; }

        [SitecoreField(Constants.ChooseCountry.IncorrectLocationText_FieldId)]
        string IncorrectLocationText { get; set; }

        [SitecoreChildren(TemplateId = Constants.Region.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<IRegion> Regions { get; set; }

        [SitecoreField(Constants.ChooseCountry.RestOfTheWorldText_FieldId)]
        string RestOfTheWorldText { get; set; }

        string CurrentCountryName { get; set; }

        string CurrentCountryIso { get; set; }
    }
}