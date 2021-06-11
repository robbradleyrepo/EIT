namespace LionTrust.Feature.Onboarding.Models
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

        [SitecoreField(Constants.ChooseCountry.CorrectLoctionText_FieldId)]
        string CorrectLoctionText { get; set; }

        [SitecoreField(Constants.ChooseCountry.IncorrectLoctionText_FieldId)]
        string IncorrectLocationText { get; set; }

        [SitecoreChildren(TemplateId = Constants.Region.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<IRegion> Regions { get; set; }

        [SitecoreField(Constants.ChooseCountry.RestOfTheWorldText_FieldId)]
        string RestOfTheWorldText { get; set; }

        string CurrentCountry { get; set; }


    }
}