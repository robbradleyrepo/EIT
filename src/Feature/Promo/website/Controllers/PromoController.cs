namespace LionTrust.Feature.Promo.Controllers
{
    using System.Web.Mvc;

    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Promo.Models;
    using LionTrust.Foundation.Indexing.Repositories;
    using LionTrust.Foundation.Legacy.Models;
    using Sitecore.Mvc.Controllers;
    using Sitecore.Mvc.Presentation;

    public class PromoController : SitecoreController
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IMvcContext _mvcContext;

        public PromoController(IMvcContext mvcContext) : this(new SearchRepository(RenderingContext.Current.ContextItem), mvcContext)
        {
        }

        public PromoController(ISearchRepository searchRepository, IMvcContext mvcContext)
        {
            this._searchRepository = searchRepository;
            this._mvcContext = mvcContext;
        }

        public ActionResult ArticleScroller()
        {
            var articleScrollerViewModel = new ArticleScrollerViewModel();
            articleScrollerViewModel.ArticleScroller = _mvcContext.GetDataSourceItem<IArticleScroller>();
            if (articleScrollerViewModel.ArticleScroller != null && articleScrollerViewModel.ArticleScroller.SelectedArticles != null)
            {
                articleScrollerViewModel.ArticleList = articleScrollerViewModel.ArticleScroller.SelectedArticles;
            }
            else if (articleScrollerViewModel.ArticleScroller.SelectedTags != null)
            {
                // Search by articleScrollerViewModel.ArticleScroller.SelectedTags
            }
            else
            {
                var articleItem = _mvcContext.GetPageContextItem<IArticle>();
                var articleTags = articleItem.PageTags;
                // Search articles by articleTags
            }

            return View("~/Views/Promo/ArticleScroller.cshtml", articleScrollerViewModel);
        }
    }
}