namespace LionTrust.Feature.MyPreferences.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System;

    public interface IRegisterInvestor : IGlassBase
    {
        [SitecoreField(Constants.RegisterInvestor.Content.IntroductionText_FieldId)]
        string IntroductionText { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Content.UserExistsErrorLabel_FieldId)]
        string UserExistsErrorLabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Content.GenericErrorLabel_FieldId)]
        string GenericErrorLabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Content.DefaultSFOrganisationId_FieldId)]
        string DefaultSFOrganisationId { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Content.CompanyFieldDefaultValue_FieldId)]
        string CompanyFieldDefaultValue { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Content.SubmitCTAText_FieldId)]
        string SubmitCTAText { get; set; }

        IPrivacyNotice PrivacyNotice { get; set; }

        [SitecoreField(Constants.RegisterInvestor.AboutYou.AboutYouTitle_FieldId)]
        string AboutYouTitle { get; set; }

        [SitecoreField(Constants.RegisterInvestor.AboutYou.YourRoleLabel_FieldId)]
        string YourRoleLabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.AboutYou.YourCountryLabel_FieldId)]
        string YourCountryLabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.AboutYou.NotCorrectLabel_FieldId)]
        string NotCorrectLabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.AboutYou.ChangeCTAText_FieldId)]
        string ChangeCTAText { get; set; }

        [SitecoreField(Constants.RegisterInvestor.ContentPreferences.ContentPreferencesTitle_FieldId)]
        string ContentPreferencesTitle { get; set; }

        [SitecoreField(Constants.RegisterInvestor.ContentPreferences.ContentPreferencesSubtitle_FieldId)]
        string ContentPreferencesSubtitle { get; set; }

        [SitecoreField(Constants.RegisterInvestor.EmailPreferences.EmailPreferencesTitle_FieldId)]
        string EmailPreferencesTitle { get; set; }

        [SitecoreField(Constants.RegisterInvestor.EmailPreferences.EmailPreferencesSubtitle_FieldId)]
        string EmailPreferencesSubtitle { get; set; }

        [SitecoreField(Constants.RegisterInvestor.EmailPreferences.SubscribeText_FieldId)]
        string SubscribeText { get; set; }

        [SitecoreField(Constants.RegisterInvestor.EmailPreferences.FirstNameLabel_FieldId)]
        string FirstNameLabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.EmailPreferences.LastNameLabel_FieldId)]
        string LastNameLabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.EmailPreferences.EmailAddressLabel_FieldId)]
        string EmailAddressLabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.EmailPreferences.FCALabel_FieldId)]
        string FCALabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.EmailPreferences.CompanyNameLabel_FieldId)]
        string CompanyNameLabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.EmailPreferences.OptOutText_FieldId)]
        string OptOutText { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Pages.ResendEmailSuccessPage_FieldId)]
        IGlassBase ResendEmailSuccessPage { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Pages.ResendEmailFailedPage_FieldId)]
        IGlassBase ResendEmailFailedPage { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Pages.ConfirmationPage_FieldId)]
        IGlassBase ConfirmationPage { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Pages.EditPreferencesPage_FieldId)]
        IGlassBase EditPreferencesPage { get; set; }
        [SitecoreField(Constants.RegisterInvestor.Pages.FundDashboardPage_FieldId)]
        IGlassBase FundDashboardPage { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Emails.UKEmailTemplate_FieldId)]
        IEditEmailPrefTemplate UKEmailTemplate { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Emails.NonUKEmailTemplate_FieldId)]
        IEditEmailPrefTemplate NonUKEmailTemplate { get; set; }

        [SitecoreField(Constants.RegisterInvestor.Emails.ResendEditPreferencesEmailTemplate_FieldId)]
        IEditEmailPrefTemplate ResendEditPreferencesEmailTemplate { get; set; }

        [SitecoreField(Constants.RegisterInvestor.RetrievePreferences.BannerTitle_FieldId)]
        string RetrievePreferencesBannerTitle { get; set; }

        [SitecoreField(Constants.RegisterInvestor.RetrievePreferences.BannerCTAText)]
        string RetrievePreferencesBannerCTAText { get; set; }

        [SitecoreField(Constants.RegisterInvestor.RetrievePreferences.IntroductionText)]
        string RetrievePreferencesIntroductionText { get; set; }

        [SitecoreField(Constants.RegisterInvestor.RetrievePreferences.EmailLabel)]
        string RetrievePreferencesEmailLabel { get; set; }

        [SitecoreField(Constants.RegisterInvestor.RetrievePreferences.CTAText)]
        string RetrievePreferencesCTAText { get; set; }

        [SitecoreField(Constants.RegisterInvestor.RetrievePreferences.EmailNotRecognized)]
        string RetrievePreferencesEmailNotRecognized { get; set; }

        [SitecoreField(Constants.RegisterInvestor.RetrievePreferences.RetrievePreferencesGoal_FieldId)]
        Guid RetrievePreferencesGoal { get; set; }
    }
}