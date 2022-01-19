namespace LionTrust.Feature.Navigation.Controllers
{
    using Glass.Mapper.Sc.Web;
    using LionTrust.Feature.Navigation.Models;
    using LionTrust.Feature.Navigation.Services;
    using LionTrust.Foundation.Schema.Models;
    using Sitecore.Mvc.Controllers;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class BreadcrumbController : SitecoreController
    {
        private readonly IRequestContext context;
        private readonly IBreadcrumbService breadcrumbService;

        public BreadcrumbController(IRequestContext context, IBreadcrumbService breadcrumbService)
        {
            this.context = context;
            this.breadcrumbService = breadcrumbService;
        }

        public ActionResult Render()
        {
            var currentPage = context.GetContextItem<IBreadcrumbDetailsModel>();
            if (currentPage == null)
            {
                return null;
            }

            var ancestors = breadcrumbService.GetAncestors(currentPage);

            var breadcrumbItems = new List<IBreadcrumbDetailsModel>();
            if (ancestors != null)
            {
                breadcrumbItems.AddRange(ancestors);
            }
            breadcrumbItems.Add(currentPage);
            
            var breadcrumbListSchema = GetBreadcrumbListData(breadcrumbItems);

            return View("~/views/navigation/breadcrumb.cshtml", new BreadcrumbViewModel { CurrentPage = currentPage, Ancestors = ancestors, BreadcrumbList = breadcrumbListSchema });
        }

        private BreadcrumbListSchema GetBreadcrumbListData(IEnumerable<IBreadcrumbDetailsModel> breadcrumbItems)
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
    }
}