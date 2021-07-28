namespace LionTrust.Foundation.Contact.Models.EditEmailPreferences
{
    using System.Collections.Generic;

    public class SFProcess
    {
        public string SFProcessId { get; set; }
        public string SFProcessName { get; set; }
        public bool IsProcessSelected { get; set; }
        public List<SFFund> SFFundList { get; set; }

        public SFProcess()
        {
            SFFundList = new List<SFFund>();
        }
    }
}