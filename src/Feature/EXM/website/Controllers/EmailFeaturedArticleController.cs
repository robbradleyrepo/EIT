using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using Sitecore.Mvc.Controllers;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailFeaturedArticleController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailFeaturedArticleController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult FeaturedArticle()
        {
            var model = _mvcContext.GetDataSourceItem<IFeaturedArticle>();
            return View("~/Views/EXM/FeaturedArticle.cshtml", model);
        }
    }
}