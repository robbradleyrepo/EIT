namespace LionTrust.Feature.Search.Routes
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Sitecore.Configuration;
    using Sitecore.Pipelines;

    public class RegisterRoutes
    {
        /// <summary>
        /// Routing for search API endpoints    
        /// </summary>
        /// <param name="args"></param>
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("Feature.Search.ArticleFacets", $"{Settings.GetSetting(Constants.Settings.ArticleApiRoute_SettingName)}/Facets",
                new
                {
                    controller = "SearchAPI",
                    action = "GetArticleListingFacets"
                });
            RouteTable.Routes.MapRoute("Feature.Search.FilteredArticles", $"{Settings.GetSetting(Constants.Settings.ArticleApiRoute_SettingName)}/Search",
                new
                {
                    controller = "SearchAPI",
                    action = "GetFilteredArticles"
                });
            RouteTable.Routes.MapRoute("Feature.Search.FundFacets", "FundSearchApi/Facets",
                new
                {
                    controller = "SearchAPI",
                    action = "GetFundListingFacets"
                });
            RouteTable.Routes.MapRoute("Feature.Search.FilteredFunds", "FundSearchApi/Search",
                new
                {
                    controller = "SearchAPI",
                    action = "GetFilteredFunds"
                });
        }
    }
}
