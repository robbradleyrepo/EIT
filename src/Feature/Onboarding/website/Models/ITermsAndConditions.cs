﻿namespace LionTrust.Feature.Onboarding.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    [SitecoreType(TemplateId = Constants.TermsAndConditions.TemplateId)]
    public interface ITermsAndConditions
    {
        [SitecoreField(Constants.TermsAndConditions.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.TermsAndConditions.ButtonText_FieldId)]
        string ButtonText { get; set; }

        [SitecoreField(Constants.TermsAndConditions.AcceptanceText_FieldId)]
        string AcceptanceText { get; set; }
    }
}