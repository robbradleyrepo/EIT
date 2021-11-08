using LionTrust.Foundation.Contact.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LionTrust.Feature.MyPreferences.Models
{
    public class RegisterInvestorViewModel
    {
        public RegisterInvestorViewModel(IRegisterInvestor content)
        {
            Content = content;
            SubscribeToEmail = true;
        }

        public IRegisterInvestor Content { get; set; }

        public bool UserExists { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }

        public string CompanyName { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string CompanyId { get; set; }

        public bool ProfessionalInvestor { get; set; } = true;

        public string Error { get; set; }

        public string InvestorType { get; set; }

        [Required]
        public string CountryName { get; set; }

        public string ChangeInvestorUrl { get; set; }

        public bool SubscribeToEmail { get; set; }
    }
}