namespace LionTrust.Feature.Article.Controllers
{
    using Glass.Mapper.Sc.Web.Mvc;
    using Sitecore.Mvc.Controllers;
    using System.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using System.Linq;

    public class FeaturedArticleHeroController : SitecoreController
    {
        private readonly IMvcContext _context;

        public FeaturedArticleHeroController(IMvcContext context)
        {
            _context = context;
        }

        public ActionResult Render()
        {
            var featuredArticleHero = _context.GetDataSourceItem<IFeaturedArticleHero>();

            if (featuredArticleHero == null)
            {
                return null;
            }

            if (featuredArticleHero.Article != null && featuredArticleHero.Article.Authors != null && featuredArticleHero.Article.Authors.Any())
            {
                featuredArticleHero.Article.Author = featuredArticleHero.Article.Authors.First();
            }

            return View("/views/article/featuredarticlehero.cshtml", featuredArticleHero);
        }
    }
}
