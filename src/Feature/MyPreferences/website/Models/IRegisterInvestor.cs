namespace LionTrust.Feature.MyPreferences.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    public interface IRegisterInvestor : IGlassBase
    {
        [SitecoreField(Constants.RegisterInvestor.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Subtitle_FieldId)]
        string Subtitle { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Description_FieldId)]
        string Description { get; set; }

        [SitecoreField(Constants.RegisterInvestor.UserExistsErrorLabel_FieldId)]
        string UserExistsErrorLabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.GenericErrorLabel_FieldId)]
        string GenericErrorLabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.DefaultSFOrganisationId_FieldId)]
        string DefaultSFOrganisationId { get; set; }

        [SitecoreField(Constants.RegisterInvestor.CompanyFieldDefaultValue_FieldId)]
        string CompanyFieldDefaultValue { get; set; }

    }
}