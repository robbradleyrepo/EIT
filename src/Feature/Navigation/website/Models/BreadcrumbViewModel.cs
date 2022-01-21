using LionTrust.Foundation.Schema.Models;

namespace LionTrust.Feature.Navigation.Models
{
    public class BreadcrumbViewModel
    {
        public IBreadcrumbDetailsModel CurrentPage { get; set; }

        public IBreadcrumbDetailsModel[] Ancestors { get; set; }

        public BreadcrumbListSchema BreadcrumbList { get; set; }
    }
}