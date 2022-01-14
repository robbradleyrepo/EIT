using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using Sitecore.Mvc.Controllers;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailPromoBannerController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailPromoBannerController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult PromoBanner()
        {
            var model = _mvcContext.GetDataSourceItem<IPromoBanner>();
            return View("~/Views/EXM/PromoBanner.cshtml", model);
        }
    }
}