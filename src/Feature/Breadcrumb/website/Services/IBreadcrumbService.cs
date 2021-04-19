using LionTrust.Feature.Breadcrumb.Models;
using System.Collections.Generic;

namespace LionTrust.Feature.Breadcrumb.Services
{
    public interface IBreadcrumbService
    {
        IBreadcrumbDetailsModel[] GetAncestors(IBreadcrumbDetailsModel source);
    }
}
