namespace LionTrust.Feature.Breadcrumb.Services
{
    using LionTrust.Feature.Breadcrumb.Models;
    using LionTrust.Foundation.DI;
    using System.Collections.Generic;

    [Service(ServiceType = typeof(IBreadcrumbService), Lifetime = Lifetime.Singleton)]
    public class BreadcrumbService : IBreadcrumbService
    {
        public IBreadcrumbDetailsModel[] GetAncestors(IBreadcrumbDetailsModel source)
        {
            return GetAncestors(source, new List<IBreadcrumbDetailsModel>());
        }

        private static IBreadcrumbDetailsModel[] GetAncestors(IBreadcrumbDetailsModel source, List<IBreadcrumbDetailsModel> ancestorList)
        {
            if (source.Parent != null)
            {
                if (source.Parent.IncludeInBreadcrumb)
                {
                    ancestorList.Add(source.Parent);
                }
            }
            else
            {
                return ancestorList.ToArray();
            }

            return GetAncestors(source.Parent, ancestorList);
        }
    }
}