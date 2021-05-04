namespace LionTrust.Feature.Search.Routes
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Sitecore.Pipelines;

    public class RegisterRoutes
    {
        /// <summary>
        /// Routing for search API endpoints    
        /// </summary>
        /// <param name="args"></param>
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("Feature.Search.Facets", "ArticleSearchApi/Facets",
                new
                {
                    controller = "SearchAPI",
                    action = "GetArticleListingFacets"
                });
            RouteTable.Routes.MapRoute("Feature.Search.FilteredArticles", "ArticleSearchApi/Search",
                new
                {
                    controller = "SearchAPI",
                    action = "GetFilteredArticles"
                });
        }
    }
}
