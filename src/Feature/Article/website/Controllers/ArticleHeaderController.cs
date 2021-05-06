using Glass.Mapper.Sc.Web.Mvc;
using LionTrust.Feature.Article.Models;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace LionTrust.Feature.Article.Controllers
{
    public class ArticleHeaderController: SitecoreController
    {
        private readonly IMvcContext context;

        public ArticleHeaderController(IMvcContext context)
        {
            this.context = context;
        }

        public ActionResult Render()
        {
            var article = context.GetContextItem<Models.IArticle>();
            if (article == null)
            {
                return null;
            }

            var componentData = context.GetDataSourceItem<IArticleHeader>();
            if (componentData == null)
            {
                return null;
            }

            return View("/views/article/articleheader.cshtml", new ArticleViewModel { ComponentData = componentData, ArticleData = article });
        }
    }
}