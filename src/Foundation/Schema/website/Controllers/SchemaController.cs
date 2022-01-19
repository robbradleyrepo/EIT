using LionTrust.Foundation.Schema.Helpers;
using LionTrust.Foundation.Schema.Models;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace LionTrust.Foundation.SchemaOrg.Controllers
{
    public class SchemaController : SitecoreController
    {
        public ActionResult OrganizationSchema(OrganizationSchema organizationSchema)
        {
            var schema = SchemaHelper.GetOrganizationSchema(organizationSchema);
            return PartialView("~/Views/Schema/_OrganizationSchema.cshtml", schema);
        }

        public ActionResult BreadcrumbListSchema()
        {
            var schema = SchemaHelper.GetBreadcrumbListSchema();
            return PartialView("~/Views/Schema/_BreadcrumbListSchema.cshtml", schema);
        }

        public ActionResult ArticleSchema()
        {
            var schema = SchemaHelper.GetArticleSchema();
            return PartialView("~/Views/Schema/_ArticleSchema.cshtml", schema);
        }
    }
}