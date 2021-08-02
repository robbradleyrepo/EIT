using System;
using System.ComponentModel.DataAnnotations;
using static LionTrust.Foundation.Onboarding.Constants;

namespace LionTrust.Feature.MyPreferences.Models
{
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

        public string CompanyId { get; set; }

        [Required]
        public bool UKResident { get; set; }

        public InvestorType InvestorType { get; set; }
    }
}