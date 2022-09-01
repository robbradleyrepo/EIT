using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using LionTrust.Feature.EXM.ViewModels;
using Sitecore.Mvc.Controllers;
using LionTrust.Feature.EXM.Extensions;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailHeaderController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailHeaderController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult Header()
        {
            var currentItem = _mvcContext.ContextItem;
            var preHeader = _mvcContext.GetDataSourceItem<IHeader>();

            var messageUrl = currentItem.GetMessageUrl();
            var model = new HeaderViewModel
            {
                PreHeader = preHeader,
                PreHeaderText = preHeader?.Text?.Replace(Constants.Tokens.MessageInBrowserUrl, messageUrl),
            };

            return View("~/Views/EXM/Header.cshtml", model);
        }
    }
}