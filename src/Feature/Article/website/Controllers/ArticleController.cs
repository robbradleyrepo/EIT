namespace LionTrust.Feature.Article.Controllers
{
    using System.Web.Mvc;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using Sitecore.Mvc.Controllers;
    
    public class ArticleController : SitecoreController
    {
        private readonly IMvcContext _mvcContext;

        public ArticleController(IMvcContext context)
        {
            this._mvcContext = context;
        }

        public ActionResult ArticleScroller()
        {
            var articleScrollerViewModel = new ArticleScrollerViewModel();
            articleScrollerViewModel.ArticleScroller = _mvcContext.GetDataSourceItem<IArticleScroller>();
            if (articleScrollerViewModel.ArticleScroller.SelectedArticles != null)
            {
                articleScrollerViewModel.ArticleList = articleScrollerViewModel.ArticleScroller.SelectedArticles;
            }

            return View("~/Views/Carousel/ArticleScroller.cshtml", articleScrollerViewModel);
        }
    }
}