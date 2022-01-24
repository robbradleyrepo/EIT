using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.EXM.Models;
using Sitecore.Mvc.Controllers;

namespace LionTrust.Feature.EXM.Controllers
{
    public class EmailArticleController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public EmailArticleController(IMvcContext context)
        {
            _mvcContext = context;
        }

        public ActionResult ArticleCardsInline()
        {
            var model = _mvcContext.GetDataSourceItem<IArticleCards>();
            return View("~/Views/EXM/ArticleCardsInline.cshtml", model);
        }

        public ActionResult ArticleCardsList()
        {
            var model = _mvcContext.GetDataSourceItem<IArticleCards>();
            return View("~/Views/EXM/ArticleCardsList.cshtml", model);
        }

        public ActionResult ArticleCardsBlock()
        {
            var model = _mvcContext.GetDataSourceItem<IArticleCards>();
            return View("~/Views/EXM/ArticleCardsBlock.cshtml", model);
        }
    }
}