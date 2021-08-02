using System.ComponentModel.DataAnnotations;
using static LionTrust.Feature.Onboarding.Constants;

namespace LionTrust.Feature.Onboarding.Models
{
    public class OnboardingSubmit
    {
        [Required(AllowEmptyStrings = false)]
        public string Country { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Foundation.Onboarding.Constants.InvestorType InvestorType { get; set; }
    }
}