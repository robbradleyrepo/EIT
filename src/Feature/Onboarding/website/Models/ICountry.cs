namespace LionTrust.Feature.Onboarding.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    [SitecoreType(TemplateId = Constants.Country.TemplateId)]
    public interface ICountry : IGlassBase
    {
        [SitecoreField(Constants.Country.CountryName_FieldId)]
        string CountryName { get; set; }

        [SitecoreField(Constants.Country.ISO_FieldId)]
        string ISO { get; set; }

        [SitecoreField(Constants.Country.TermsAndConditions_FieldId)]
        string TermsAndConditions { get; set; }
    }
}
