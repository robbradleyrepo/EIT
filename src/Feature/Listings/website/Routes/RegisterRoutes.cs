namespace LionTrust.Feature.Listings.Routes
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Sitecore.Configuration;
    using Sitecore.Pipelines;

    public class RegisterRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("Feature.Listings.DownloadDocuments", $"{Settings.GetSetting(Constants.Settings.DocumentApiRoute_SettingName)}/DownloadDocuments",
                new
                {
                    controller = "Documents",
                    action = "DownloadDocuments"
                });
            RouteTable.Routes.MapRoute("Feature.Listings.GetDocuments", $"{Settings.GetSetting(Constants.Settings.DocumentApiRoute_SettingName)}/GetDocuments",
                new
                {
                    controller = "Documents",
                    action = "GetDocuments"
                });
        }
    }
}