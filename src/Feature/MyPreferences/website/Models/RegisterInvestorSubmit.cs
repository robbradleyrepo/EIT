namespace LionTrust.Feature.MyPreferences.Models
{
    using LionTrust.Foundation.Contact.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;

    public class RegisterInvestorSubmit
    {
        public Guid DatasourceId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string CompanyName { get; set; }

        [StringLength(6, MinimumLength = 6)]
        public string CompanyId { get; set; }

        public bool ProfessionalInvestor { get; set; }

        [Setting, DefaultValue(default(List<SFProcess>))]
        public IEnumerable<SFProcess> SFProcessList { get; set; }

        public bool SubscribeToEmail { get; set; }

    }
}