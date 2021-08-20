namespace LionTrust.Foundation.Onboarding.Models
{
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;
    using System.Collections.Generic;

    [SitecoreType(TemplateId = Constants.ChooseInvestorRole.TemplateId)]
    public interface IChooseInvestorRole : IGlassBase
    {
        [SitecoreField(Constants.ChooseInvestorRole.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.ChooseInvestorRole.PrivateInvestorButtonText_FieldId)]
        string PrivateInvestorButtonText { get; set; }

        [SitecoreField(Constants.ChooseInvestorRole.ProfessionalInvestorButtonText_FieldId)]
        string ProfessionalInvestorButtonText { get; set; }

        [SitecoreField(Constants.ChooseInvestorRole.ModalCTAText_FieldId)]
        string ModalCTAText { get; set; }

        [SitecoreField(Constants.ChooseInvestorRole.NonUKTitle_FieldId)]
        string NonUKTitle { get; set; }

        [SitecoreField(Constants.ChooseInvestorRole.ModalTitle_FieldId)]
        string ModalTitle { get; set; }

        [SitecoreField(Constants.ChooseInvestorRole.ModalIntroText_FieldId)]
        string ModalIntroText { get; set; }

        [SitecoreField(Constants.ChooseInvestorRole.ModalBodyText_FieldId)]
        string ModalBodyText { get; set; }

        [SitecoreField(Constants.ChooseInvestorRole.ModalButtonText_FieldId)]
        string ModalButtonText { get; set; }

        [SitecoreChildren(TemplateId = Constants.Investor.TemplateId, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
        IEnumerable<IInvestor> Investors { get; set; }
    }
}