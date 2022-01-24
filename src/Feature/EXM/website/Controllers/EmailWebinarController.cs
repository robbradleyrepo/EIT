using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using Sitecore.Mvc.Controllers;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailWebinarController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailWebinarController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult RegisterWebinar()
        {
            var model = _mvcContext.GetDataSourceItem<IRegisterWebinar>();
            return View("~/Views/EXM/RegisterWebinar.cshtml", model);
        }

        public ActionResult Webinars()
        {
            var model = _mvcContext.GetDataSourceItem<IWebinarCards>();
            return View("~/Views/EXM/Webinars.cshtml", model);
        }
    }
}