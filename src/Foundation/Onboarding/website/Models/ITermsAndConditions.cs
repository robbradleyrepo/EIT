namespace LionTrust.Foundation.Onboarding.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;
    using System;

    [SitecoreType(TemplateId = Constants.TermsAndConditions.TemplateId)]
    public interface ITermsAndConditions
    {
        [SitecoreField(Constants.TermsAndConditions.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.TermsAndConditions.ButtonText_FieldId)]
        string ButtonText { get; set; }

        [SitecoreField(Constants.TermsAndConditions.AcceptanceCheckboxText_FieldId)]
        string AcceptanceCheckboxText { get; set; }

        [SitecoreField(Constants.TermsAndConditions.AcceptanceFirstText_FieldId)]
        string AcceptanceFirstText { get; set; }

        [SitecoreField(Constants.TermsAndConditions.AcceptanceLink_FieldId)]
        Link AcceptanceLink { get; set; }

        [SitecoreField(Constants.TermsAndConditions.AcceptanceSecondText_FieldId)]
        string AcceptanceSecondText { get; set; }

        [SitecoreField(Constants.TermsAndConditions.Goal_FieldId)]
        IGlassBase Goal { get; set; }

        [SitecoreId]
        Guid Id { get; set; }

        bool AcceptanceCheckbox { get; set; }
    }
}
