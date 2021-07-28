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

        public IRegisterInvestor Content { get; set; }

        public string GenericErrorLabel { get; set; }

        public string UserExistsErrorLabel { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-] +)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        public string CompanyName { get; set; }

        public string CompanyId { get; set; }

        [Required]
        public bool UKResident { get; set; }

        public InvestorType InvestorType { get; set; }

        public string Error { get; set; }
    }
}