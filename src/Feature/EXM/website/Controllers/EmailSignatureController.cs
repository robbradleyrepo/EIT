using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using Sitecore.Mvc.Controllers;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailSignatureController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailSignatureController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult Signature()
        {
            var model = _mvcContext.GetDataSourceItem<ISignature>();
            return View("~/Views/EXM/Signature.cshtml", model);
        }
    }
}