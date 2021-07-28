namespace LionTrust.Foundation.Contact.Models.Labels
{
    using LionTrust.Foundation.Contact.Models.Types;

    public class EditEmailPreferencesLabels
    {
        public IProperty<string> TitleText { get; set; }
        public IProperty<string> ProcessListTitle { get; set; }
        public IProperty<string> ProcessListSubTitle { get; set; }
        public IProperty<string> CheckboxInstructionText { get; set; }
        public IProperty<string> LTNewsLabel { get; set; }
        public IProperty<string> LTNewsLabelSubtitle { get; set; }
        public IProperty<string> LTUnsubscribeAllLabel { get; set; }
        public IProperty<string> InstitutionalBulletinLabel { get; set; }
        public IProperty<string> InstitutionalBulletinSubtitle { get; set; }
        public IProperty<string> GlobalSelectAllCheckboxLabel { get; set; }
        public IProperty<string> PrivacyPolicyText { get; set; }
        public IProperty<string> PageLoadErrorText { get; set; }
    }
}
