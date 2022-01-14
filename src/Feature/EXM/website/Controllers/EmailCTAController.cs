using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using Sitecore.Mvc.Controllers;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailCTAController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailCTAController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult CTA()
        {
            var model = _mvcContext.GetDataSourceItem<ICta>();
            return View("~/Views/EXM/CTA.cshtml", model);
        }
    }
}