namespace LionTrust.Foundation.Navigation
{
    using LionTrust.Foundation.Navigation.Models;
    using LionTrust.Foundation.Onboarding.Helpers;
    using System.Collections.Generic;
    using System.Linq;

    public static class Extensions
    {
        public static IEnumerable<INavigablePage> WithAccessTo(this IEnumerable<INavigablePage> pages)
        {
            return pages.Where(x => x.Fund == null || OnboardingHelper.HasAccess(x.Fund.ExcludedCountries));
        }
    }
}