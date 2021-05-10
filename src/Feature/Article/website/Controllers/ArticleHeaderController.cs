namespace LionTrust.Feature.Article.Controllers
{
    using System.Web.Mvc;

    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Foundation.Article.Models;
    using Sitecore.Mvc.Controllers;

    public class ArticleHeaderController: SitecoreController
    {
        private readonly IMvcContext context;

        public ArticleHeaderController(IMvcContext context)
        {
            this.context = context;
        }

        public ActionResult Render()
        {
            var article = context.GetContextItem<IArticle>();
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