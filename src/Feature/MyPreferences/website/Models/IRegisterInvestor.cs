namespace LionTrust.Feature.MyPreferences.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.Contact.Models;
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

        [SitecoreField(Constants.RegisterInvestor.ResendEmailSuccessPage_FieldId)]
        IGlassBase ResendEmailSuccessPage { get; set; }

        [SitecoreField(Constants.RegisterInvestor.ResendEmailFailedPage_FieldId)]
        IGlassBase ResendEmailFailedPage { get; set; }

        [SitecoreField(Constants.RegisterInvestor.ConfirmationPage_FieldId)]
        IGlassBase ConfirmationPage { get; set; }

        [SitecoreField(Constants.RegisterInvestor.EditPreferencesPage_FieldId)]
        IGlassBase EditPreferencesPage { get; set; }
        [SitecoreField(Constants.RegisterInvestor.FundDashboardPage_FieldId)]
        IGlassBase FundDashboardyPage { get; set; }

        [SitecoreField(Constants.RegisterInvestor.UKEmailTemplate_FieldId)]
        IEditEmailPrefTemplate UKEmailTemplate { get; set; }

        [SitecoreField(Constants.RegisterInvestor.NonUKEmailTemplate_FieldId)]
        IEditEmailPrefTemplate NonUKEmailTemplate { get; set; }

        [SitecoreField(Constants.RegisterInvestor.ResendEditPreferencesEmailTemplate_FieldId)]
        IEditEmailPrefTemplate ResendEditPreferencesEmailTemplate { get; set; }


    }
}