using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using Sitecore.Mvc.Controllers;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailRichTextContentController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailRichTextContentController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult RichTextContent()
        {
            var model = _mvcContext.GetDataSourceItem<IRichTextContent>();
            return View("~/Views/EXM/RichTextContent.cshtml", model);
        }
    }
}