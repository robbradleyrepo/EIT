using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using LionTrust.Foundation.SitecoreExtensions.Extensions;
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

        public ActionResult WebinarCards()
        {
            var model = _mvcContext.GetDataSourceItem<IWebinarCards>();
            
            foreach(var webinar in model.Webinars)
            {
                webinar.Title = webinar.Title.Ellipsis(Constants.CharatersLimit.WebinarTitle);
                webinar.Speakers = webinar.Speakers.Ellipsis(Constants.CharatersLimit.WebinarSpeakers);
            }

            return View("~/Views/EXM/WebinarCards.cshtml", model);
        }
    }
}