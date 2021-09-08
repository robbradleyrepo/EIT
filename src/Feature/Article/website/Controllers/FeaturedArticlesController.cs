namespace LionTrust.Feature.Article.Controllers
{
    using Glass.Mapper.Sc.Web;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Article.FeaturedArticleMappers;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;
    using LionTrust.Foundation.Onboarding.Helpers;

    public class FeaturedArticlesController: SitecoreController
    {
        private readonly IMvcContext context;
        private readonly IArticleContentSearchService contentSearchService;
        private readonly BaseLinkManager linkManager;
        private readonly string databaseName;

        public FeaturedArticlesController(IMvcContext context, IArticleContentSearchService contentSearchService, IRequestContext requestContext, BaseLinkManager linkManager)
        {
            this.context = context;
            this.contentSearchService = contentSearchService;
            this.linkManager = linkManager;
            databaseName = requestContext.SitecoreService.Database.Name;
        }

        public ActionResult Render()
        {
            var datasource = context.GetDataSourceItem<IFeaturedArticles>();
            if (datasource == null)
            {
                return null;
            }

            var result = new FeaturedArticlesViewModel { Data = datasource };

            if (datasource.Articles != null && datasource.Articles.Any())
            {
                datasource.Articles = datasource.Articles.Where(a => OnboardingHelper.HasAccess(a.Fund?.ExcludedCountries));
                result.FeaturedArticles = FeaturedArticleLink.Map(datasource);
            }
            else if (datasource.Children != null && datasource.Children.Any())
            {
                result.FeaturedArticles = UrlLink.Map(datasource);
            }
            else
            {
                result.FeaturedArticles = new SearchedRelatedArticles(contentSearchService, linkManager).Map(datasource, databaseName);
            }
            
            if ((result.FeaturedArticles == null || !result.FeaturedArticles.Any()) && !Sitecore.Context.PageMode.IsExperienceEditor)
            {
                return null;
            }

            return View("/views/article/featuredarticles.cshtml", result);
        }
    }
}