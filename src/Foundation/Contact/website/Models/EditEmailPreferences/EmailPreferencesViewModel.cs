namespace LionTrust.Foundation.Contact.Models.EditEmailPreferences
{
    using System.Collections.Generic;

    public class EmailPreferencesViewModel
    {
        public string SFEntityId { get; set; }
        public bool IsContact { get; set; }
        public string SFRandomGUID { get; set; }
        public string EmailAddress { get; set; }
        public bool IncludeInLTNews { get; set; }
        public bool UnsubscribeAll { get; set; }
        public bool IsConsentGivenDateEmpty { get; set; }
        public bool IsUkResident { get; set; }
        public bool IsInstitutionalBulletinChecked { get; set; }
        public string MainTitleText { get; set; }
        public string TitleText { get; set; }
        public string ProcessListTitle { get; set; }
        public string ProcessListSubTitle { get; set; }
        public string CheckboxInstructionText { get; set; }
        public string GlobalSelectAllCheckboxLabel { get; set; }
        public string PrivacyPolicyText { get; set; }
        public string LTNewsLabel { get; set; }
        public string LTNewsLabelSubtitle { get; set; }
        public string InstitutionalBulletinLabel { get; set; }
        public string InstitutionalBulletinLabelSubtitle { get; set; }
        public string LTUnsubscribeAllLabel { get; set; }
        public List<SFProcessEmailPreferenceViewModel> SFProcessList { get; set; }
        public bool IsDataRetrievalSuccess { get; set; }
        public string PageLoadErrorText { get; set; }

        public EmailPreferencesViewModel()
        {
            SFProcessList = new List<SFProcessEmailPreferenceViewModel>();
        }
    }
}