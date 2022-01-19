using LionTrust.Foundation.Schema.Models;

namespace LionTrust.Feature.Navigation.Models
{
    public class NavigationViewModel
    {
        public IHome HomeItem { get; set; }
        public OrganizationSchema Organization { get; set; }
    }
}