namespace LionTrust.Feature.Promo.Controllers
{
    using System.Web.Mvc;

    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Carousel.Models;
    using LionTrust.Foundation.Indexing.Repositories;
    using Sitecore.Mvc.Controllers;
    using Sitecore.Mvc.Presentation;

    public class CarouselController : SitecoreController
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IMvcContext _mvcContext;

        public CarouselController(IMvcContext mvcContext) : this(new SearchRepository(RenderingContext.Current.ContextItem), mvcContext)
        {
        }

        public CarouselController(ISearchRepository searchRepository, IMvcContext mvcContext)
        {
            this._searchRepository = searchRepository;
            this._mvcContext = mvcContext;
        }

        public ActionResult ArticleScroller()
        {
            var articleScrollerViewModel = new ArticleScrollerViewModel();

            return View("~/Views/Carousel/ArticleScroller.cshtml", articleScrollerViewModel);
        }
    }
}