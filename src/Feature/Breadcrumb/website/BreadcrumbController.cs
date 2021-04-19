using Glass.Mapper.Sc.Web;
using LionTrust.Feature.Breadcrumb.Models;
using LionTrust.Feature.Breadcrumb.Services;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace LionTrust.Feature.Breadcrumb
{
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

            return View("~/views/breadcrumb/breadcrumb.cshtml", new BreadcrumbViewModel { CurrentPage = currentPage, Ancestors = ancestors });
        }
    }
}