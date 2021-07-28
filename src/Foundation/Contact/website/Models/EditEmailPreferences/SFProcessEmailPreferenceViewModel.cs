namespace LionTrust.Foundation.Contact.Models.EditEmailPreferences
{
    using System.Collections.Generic;

    public class SFProcessEmailPreferenceViewModel
    {
        public string SFProcessId { get; set; }
        public string SFProcessName { get; set; }
        public bool IsProcessSelected { get; set; }
        public List<SFFundViewModel> SFFundList { get; set; }

        public SFProcessEmailPreferenceViewModel()
        {
            SFFundList = new List<SFFundViewModel>();
        }
    }
}