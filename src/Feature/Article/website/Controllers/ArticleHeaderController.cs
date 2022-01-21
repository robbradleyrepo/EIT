namespace LionTrust.Feature.Article.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Article.Repositories;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.Mvc.Controllers;

    public class ArticleHeaderController: SitecoreController
    {
        private readonly IMvcContext context;
        private readonly IArticleContentSearchService _contentSearchService;

        public ArticleHeaderController(IMvcContext context, IArticleContentSearchService contentSearchService)
        {
            this.context = context;
            _contentSearchService = contentSearchService;
        }

        public ActionResult Render()
        {
            var article = context.GetContextItem<IArticle>();
            if (article == null)
            {
                return null;
            }

            if (article.Authors.Any())
            {
                article.Author = article.Authors.First();
            }

            var componentData = context.GetDataSourceItem<IArticleHeader>();

            var articleSchema = new ArticleRepository(_contentSearchService, context).GetArticleSchemaData(article);

            return View("/views/article/articleheader.cshtml", new ArticleViewModel { ComponentData = componentData, ArticleData = article, ArticleSchema = articleSchema });
        }        
    }
}