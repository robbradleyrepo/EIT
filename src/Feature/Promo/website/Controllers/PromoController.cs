namespace LionTrust.Feature.Promo.Controllers
{
    using System.Web.Mvc;

    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Promo.Models;
    using LionTrust.Foundation.Indexing.Repositories;
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
            if (articleScrollerViewModel.ArticleScroller.SelectedArticles != null)
            {
                articleScrollerViewModel.ArticleList = articleScrollerViewModel.ArticleScroller.SelectedArticles;
            }

            return View("~/Views/Carousel/ArticleScroller.cshtml", articleScrollerViewModel);
        }
    }
}