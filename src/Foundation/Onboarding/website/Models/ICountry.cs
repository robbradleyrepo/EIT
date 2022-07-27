namespace LionTrust.Foundation.Onboarding.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System;

    [SitecoreType(TemplateId = Constants.Country.TemplateId)]
    public interface ICountry : IGlassBase
    {
        [SitecoreField(Constants.Country.CountryName_FieldId)]
        string CountryName { get; set; }

        [SitecoreField(Constants.Country.UseDefiniteArticle_FieldId)]
        bool UseDefiniteArticle { get; set; }

        [SitecoreField(Constants.Country.ISO_FieldId)]
        string ISO { get; set; }        

        [SitecoreField(Constants.Country.FundCentreCountryCode_FieldId)]
        string FundCentreCountryCode { get; set; }

        [SitecoreField(Constants.Country.Goal_FieldId)]
        Guid GoalId { get; set; }

        [SitecoreField(Constants.Country.TermsAndConditionsProfessional_FieldId)]
        string TermsAndConditionsProfessional { get; set; }

        [SitecoreField(Constants.Country.TermsAndConditionsPrivate_FieldId)]
        string TermsAndConditionsPrivate { get; set; }

        [SitecoreField(Constants.Country.TermsAndConditionsInstitutional_FieldId)]
        string TermsAndConditionsInstitutional { get; set; }

        [SitecoreField(Constants.Country.TermsAndConditionsJournalist_FieldId)]
        string TermsAndConditionsJournalist { get; set; }

        [SitecoreField(Constants.Country.TermsAndConditionsShareholder_FieldId)]
        string TermsAndConditionsShareholder { get; set; }

        [SitecoreField(Constants.Country.TermsAndConditionsCharity_FieldId)]
        string TermsAndConditionsCharity { get; set; }
    }
}
