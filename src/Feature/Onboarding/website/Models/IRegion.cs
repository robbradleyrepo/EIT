namespace LionTrust.Feature.Onboarding.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = Constants.Region.TemplateId)]
    public interface IRegion : IGlassBase
    {
        [SitecoreField(Constants.Region.Title_FieldId)]
        string RegionName { get; set; }

        [SitecoreChildren(TemplateId = Constants.Country.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<ICountry> Countries { get; set; }
    }
}
