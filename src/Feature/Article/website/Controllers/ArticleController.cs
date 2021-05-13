namespace LionTrust.Feature.Article.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Article.Repositories;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.Mvc.Controllers;
    
    public class ArticleController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;
        private readonly IArticleContentSearchService _contentSearchService;

        public ArticleController(IMvcContext context, IArticleContentSearchService contentSearchService)
        {
            _mvcContext = context;
            _contentSearchService = contentSearchService;
        }

        public ActionResult ArticleScroller()
        {
            var articleScrollerViewModel = new ArticleScrollerViewModel();
            articleScrollerViewModel.ArticleScroller = _mvcContext.GetDataSourceItem<IArticleScroller>();
            if (articleScrollerViewModel.ArticleScroller.SelectedArticles != null
                && articleScrollerViewModel.ArticleScroller.SelectedArticles.Any())
            {
                articleScrollerViewModel.ArticleList = articleScrollerViewModel.ArticleScroller.SelectedArticles;
            }
            else if (articleScrollerViewModel.ArticleScroller.SelectedTags != null
                     && articleScrollerViewModel.ArticleScroller.SelectedTags.Any())
            {
                articleScrollerViewModel.ArticleList = 
                    new ArticleRepository(_contentSearchService, _mvcContext)
                        .GetArticlePromosByTopics(articleScrollerViewModel.ArticleScroller.SelectedTags.Select(x => x.Id));
            }
            else
            {
                var currentPage = _mvcContext.GetPageContextItem<IArticle>();
                if (currentPage != null && currentPage.Topics != null && currentPage.Topics.Any())
                {
                    articleScrollerViewModel.ArticleList =
                        new ArticleRepository(_contentSearchService, _mvcContext)
                            .GetArticlePromosByTopics(currentPage.Topics.Select(x => x.Id));
                }
            }

            return View("~/Views/Article/ArticleScroller.cshtml", articleScrollerViewModel);
        }
    }
}