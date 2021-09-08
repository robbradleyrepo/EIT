namespace LionTrust.Feature.Article.Controllers
{
    using Glass.Mapper.Sc.Web;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Foundation.Onboarding.Helpers;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;

    public class RelatedArticlesController: SitecoreController
    {
        private readonly IMvcContext context;

        public RelatedArticlesController(IMvcContext context)
        {
            this.context = context;
        }

        public ActionResult Render()
        {
            var datasource = context.GetDataSourceItem<IRelatedArticles>();
            if (datasource == null || (!Sitecore.Context.PageMode.IsExperienceEditor && datasource.Articles != null && !datasource.Articles.Any()))
            {
                return null;
            }
            else
            {
                datasource.Articles = datasource.Articles.Where(a => OnboardingHelper.HasAccess(a.Fund?.ExcludedCountries))?.Take(3);
            }

            return View("/views/article/RelatedArticles.cshtml", datasource);
        }
    }
}