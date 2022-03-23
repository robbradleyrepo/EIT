using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;
using System.IO;
using System.Net;
using System.Web;

namespace LionTrust.Foundation.SitecoreExtensions.Pipelines.HttpRequestBegin
{
    public class ItemNotFoundProcessor : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (
                Context.Item != null ||
                Context.Site == null ||
                Context.Database == null ||
                File.Exists(HttpContext.Current.Server.MapPath(args.Url.FilePath))
               )
            {
                return;
            }

            // avoid api calls to return 404
            var fundSearchApi = Settings.GetSetting("Feature.Search.FundApiRoute", string.Empty).ToLowerInvariant().ToLower();
            var articleSearchApi = Settings.GetSetting("Feature.Search.ArticleApiRoute", string.Empty).ToLowerInvariant().ToLower();
            var myFundSearchApi = Settings.GetSetting("Feature.Search.MyFundsApiRoute", string.Empty).ToLowerInvariant().ToLower();
            var aiteSearchApi = Settings.GetSetting("Feature.Search.SiteSearchApiRoute", string.Empty).ToLowerInvariant().ToLower();
            var documentsApi = Settings.GetSetting("Feature.Listings.DocumentsApiRoute", string.Empty).ToLowerInvariant().ToLower();
            var genericListingApi = Settings.GetSetting("Feature.Listings.GenericListingApiRoute", string.Empty).ToLowerInvariant().ToLower();
            var mediaGalleryApi = Settings.GetSetting("Feature.Listings.MediaGalleryApiRoute", string.Empty).ToLowerInvariant().ToLower();
            var latestResultsApi = Settings.GetSetting("Feature.Listings.LatestResultsApiRoute", string.Empty).ToLowerInvariant().ToLower();
            var fieldTrackingRegister = "/fieldtracking/register";
            if (args.Url.FilePath.ToLowerInvariant().ToLower().Contains("/sitecore") ||
                args.Url.FilePath.ToLowerInvariant().ToLower().Contains(fundSearchApi) ||
                args.Url.FilePath.ToLowerInvariant().ToLower().Contains(articleSearchApi) ||
                args.Url.FilePath.ToLowerInvariant().ToLower().Contains(myFundSearchApi) ||
                args.Url.FilePath.ToLowerInvariant().ToLower().Contains(aiteSearchApi) ||
                args.Url.FilePath.ToLowerInvariant().ToLower().Contains(documentsApi) ||
                args.Url.FilePath.ToLowerInvariant().ToLower().Contains(genericListingApi) ||
                args.Url.FilePath.ToLowerInvariant().ToLower().Contains(mediaGalleryApi) ||
                args.Url.FilePath.ToLowerInvariant().ToLower().Contains(latestResultsApi) ||
                args.Url.FilePath.ToLowerInvariant().ToLower().Contains(fieldTrackingRegister))
            {
                return;
            }

            Log.Debug(this + " : No item found for " + Context.RawUrl + " (" + Context.User + " - " + Context.Device.Name + ")");

            var pageNotFoundItem = Context.Database.GetItem(string.Concat(Context.Site.StartPath, Settings.ItemNotFoundUrl));
            if (pageNotFoundItem == null)
            {
                return;
            }

            args.ProcessorItem = pageNotFoundItem;
            Context.Item = pageNotFoundItem;            
        }
    }
}