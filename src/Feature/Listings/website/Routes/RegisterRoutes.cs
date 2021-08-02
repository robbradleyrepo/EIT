namespace LionTrust.Feature.Listings.Routes
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using LionTrust.Feature.Listings;
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
            RouteTable.Routes.MapRoute("Feature.Listings.Facets", $"{Settings.GetSetting(Constants.Settings.GenericListingApiRoute_SettingName)}/Facets",
                new
                {
                    controller = "GenericListingApi",
                    action = "GetFacets"
                });
            RouteTable.Routes.MapRoute("Feature.Listings.FilteredResults", $"{Settings.GetSetting(Constants.Settings.GenericListingApiRoute_SettingName)}/Search",
                new
                {
                    controller = "GenericListingApi",
                    action = "GetFilteredResults"
                });
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
            RouteTable.Routes.MapRoute("Feature.Listings.DownloadMediaImages", $"{Settings.GetSetting(Constants.Settings.MediGalleryApiRoute_SettingName)}/DownloadMediaImages",
                new
                {
                    controller = "MediaGallery",
                    action = "DownloadMediaImages"
                });
            RouteTable.Routes.MapRoute("Feature.Listings.GetMediaItems", $"{Settings.GetSetting(Constants.Settings.MediGalleryApiRoute_SettingName)}/GetMediaItems",
                new
                {
                    controller = "MediaGallery",
                    action = "GetMediaItems"
                });
        }
    }
}
