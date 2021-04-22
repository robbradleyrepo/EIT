﻿namespace LionTrust.Feature.Navigation.Controllers
{
    using Glass.Mapper.Sc.Web;
    using LionTrust.Feature.Navigation.Models;
    using LionTrust.Feature.Navigation.Services;
    using Sitecore.Mvc.Controllers;
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

            return View("~/views/navigation/breadcrumb.cshtml", new BreadcrumbViewModel { CurrentPage = currentPage, Ancestors = ancestors });
        }
    }
}