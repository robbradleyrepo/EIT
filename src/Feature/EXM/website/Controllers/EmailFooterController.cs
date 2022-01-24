using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using Sitecore.Mvc.Controllers;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailFooterController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailFooterController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult Footer()
        {
            var model = _mvcContext.GetDataSourceItem<IFooter>();
            return View("~/Views/EXM/Footer.cshtml", model);
        }

        public ActionResult Disclaimer()
        {
            var model = _mvcContext.GetDataSourceItem<IDisclaimer>();
            return View("~/Views/EXM/Disclaimer.cshtml", model);
        }
    }
}