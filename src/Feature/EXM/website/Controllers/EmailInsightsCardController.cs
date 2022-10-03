using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using LionTrust.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Mvc.Controllers;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailInsightsCardController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailInsightsCardController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult InsightsCard()
        {
            var model = _mvcContext.GetDataSourceItem<IInsightsCard>();
            return View("~/Views/EXM/InsightsCard.cshtml", model);
        }
    }
}