namespace LionTrust.Feature.Onboarding.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OnboardingSubmit
    {
        [Required(AllowEmptyStrings = false)]
        public string Country { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Guid InvestorId { get; set; }
    }
}