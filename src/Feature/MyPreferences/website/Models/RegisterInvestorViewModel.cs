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

        public IRegisterInvestor Content { get; set; }

        public string GenericErrorLabel { get; set; }

        public string UserExistsErrorLabel { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name *")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name *")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        [Display(Name = "Email *")]
        public string Email { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Individual FCA Reference Number")]
        public string CompanyId { get; set; }

        [Required(ErrorMessage = "UK Resident is required")]
        [Display(Name = "UK Resident *")]
        public bool? UKResident { get; set; }

        public InvestorType InvestorType { get; set; }

        public string Error { get; set; }
    }
}