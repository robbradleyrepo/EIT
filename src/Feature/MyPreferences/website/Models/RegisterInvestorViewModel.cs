using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static LionTrust.Foundation.Onboarding.Constants;

namespace LionTrust.Feature.MyPreferences.Models
{
    public class RegisterInvestorViewModel
    {
        public RegisterInvestorViewModel(IRegisterInvestor content, InvestorType investorType)
        {
            Content = content;
            InvestorType = investorType;
        }

        public RegisterInvestorViewModel()
        {

        }

        public IRegisterInvestor Content { get; set; }

        public string GenericErrorLabel { get; set; }

        public string UserExistsErrorLabel { get; set; }

        [Required]
        [Display(Name = "First Name *")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name *")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email *")]
        public string Email { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Individual FCA Reference Number")]
        public string CompanyId { get; set; }

        [Required]
        [Display(Name = "UK Resident *")]
        public bool UKResident { get; set; }

        public InvestorType InvestorType { get; set; }

        public string Error { get; set; }
    }
}