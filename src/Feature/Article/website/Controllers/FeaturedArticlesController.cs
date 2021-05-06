namespace LionTrust.Feature.Article.Controllers
{
    using Glass.Mapper.Sc.Web;
    using Glass.Mapper.Sc.Web.Mvc;
    using LionTrust.Feature.Article.Models;
    using LionTrust.Feature.Article.RelatedArticleMappers;
    using LionTrust.Foundation.Search.Services.Interfaces;
    using Sitecore.Abstractions;
    using Sitecore.Mvc.Controllers;
    using System.Linq;
    using System.Web.Mvc;

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

            var result = new RelatedArticlesViewModel { Data = datasource };
            
            if (datasource.Articles != null && datasource.Articles.Any())
            {
                result.RelatedArticles = RelatedArticleLink.Map(datasource);
            }
            else if(datasource.Children != null && datasource.Children.Any())
            {
                result.RelatedArticles = UrlLink.Map(datasource);
            }
            else
            {
                var searchParameters = context.GetRenderingParameters<IArticleFilter>();
                if (searchParameters != null)
                {
                    result.RelatedArticles = new SearchedRelatedArticles(contentSearchService, linkManager).Map(searchParameters, databaseName);
                }
            }
            
            if ((result.RelatedArticles == null || !result.RelatedArticles.Any()) && !Sitecore.Context.PageMode.IsExperienceEditor)
            {
                return null;
            }

            return View("/views/article/featuredarticles.cshtml", result);
        }
    }
}