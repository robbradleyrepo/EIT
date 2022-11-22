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

        private const int MaxAuthors = 3;

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

            var componentData = context.GetDataSourceItem<IArticleHeader>();
            var articleSchema = new ArticleRepository(_contentSearchService, context).GetArticleSchemaData(article);

            var viewModel = new ArticleViewModel 
            { 
                ComponentData = componentData, 
                ArticleData = article, 
                ArticleSchema = articleSchema,
                ShowAuthors = article.Authors != null && article.Authors.Count() <= MaxAuthors
            };

            return View("/views/article/articleheader.cshtml", viewModel);
        }        
    }
}