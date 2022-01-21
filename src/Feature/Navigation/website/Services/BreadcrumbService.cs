namespace LionTrust.Feature.Navigation.Services
{
    using LionTrust.Feature.Navigation.Models;
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Schema.Models;
    using System.Collections.Generic;

    [Service(ServiceType = typeof(IBreadcrumbService), Lifetime = Lifetime.Singleton)]
    public class BreadcrumbService : IBreadcrumbService
    {
        public IBreadcrumbDetailsModel[] GetAncestors(IBreadcrumbDetailsModel source)
        {
            return GetAncestors(source, new List<IBreadcrumbDetailsModel>());
        }

        public BreadcrumbListSchema GetBreadcrumbListData(IEnumerable<IBreadcrumbDetailsModel> breadcrumbItems)
        {
            var breadcrumbListSchema = new BreadcrumbListSchema();
            if (breadcrumbItems != null)
            {
                var count = 0;
                var breadcrumbList = new List<BreadcrumbItem>();
                foreach (var item in breadcrumbItems)
                {
                    if (item != null)
                    {
                        count++;
                        var breadcrumbItem = new BreadcrumbItem()
                        {
                            Position = count,
                            Url = item.AbsoluteUrl,
                            Name = item.BreadcrumbTitle
                        };

                        breadcrumbList.Add(breadcrumbItem);
                    }
                }

                breadcrumbListSchema.BreadcrumbItems = breadcrumbList;
            }

            return breadcrumbListSchema;
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
                ancestorList.Reverse();
                return ancestorList.ToArray();
            }

            return GetAncestors(source.Parent, ancestorList);
        }
    }
}