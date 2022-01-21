using LionTrust.Feature.Navigation.Models;
using LionTrust.Foundation.Schema.Models;
using System.Collections.Generic;

namespace LionTrust.Feature.Navigation.Services
{
    public interface IBreadcrumbService
    {
        IBreadcrumbDetailsModel[] GetAncestors(IBreadcrumbDetailsModel source);

        BreadcrumbListSchema GetBreadcrumbListData(IEnumerable<IBreadcrumbDetailsModel> breadcrumbItems);
    }
}
