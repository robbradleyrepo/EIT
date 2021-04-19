namespace LionTrust.Feature.Breadcrumb.Models
{
    public class BreadcrumbViewModel
    {
        public IBreadcrumbDetailsModel CurrentPage { get; set; }

        public IBreadcrumbDetailsModel[] Ancestors { get; set; }
    }
}