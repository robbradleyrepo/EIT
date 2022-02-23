using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using Sitecore.Mvc.Controllers;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailUserNameIntroController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailUserNameIntroController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult UserNameIntro()
        {
            var model = _mvcContext.GetDataSourceItem<IUserNameIntro>();

            return View("~/Views/EXM/UserNameIntro.cshtml", model);
        }
    }
}