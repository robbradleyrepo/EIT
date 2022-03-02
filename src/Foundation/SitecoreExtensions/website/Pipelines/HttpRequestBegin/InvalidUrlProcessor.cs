namespace LionTrust.Foundation.SitecoreExtensions.Pipelines.HttpRequestBegin
{
    using Sitecore;
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using Sitecore.Links;
    using Sitecore.Pipelines.HttpRequest;
    using Sitecore.Web;
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Web;

    public class InvalidUrlProcessor : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            var request = args.Context.Request;
            var response = args.Context.Response;

            Log.Debug("InvalidUrlProcessor: Start Handling Request (args.LocalPath=" + args.LocalPath + ", args.Url.FilePath=" + args.Url.FilePath + ")"); if (!IsValidContextItemResolved())
            {
                Log.Debug("InvalidUrlProcessor: Is Not a ValidContextItem so skipping it.");
                return;
            }

            if (args.LocalPath.StartsWith("/sitecore") || args.Url.FilePath.StartsWith("/sitecore"))
            {
                Log.Debug("InvalidUrlProcessor: Skipping because the local path contains the Sitecore folder.");
                return;
            }

            if (RequestIsForPhysicalFile(args.Url.FilePath))
            {
                Log.Debug("InvalidUrlProcessor: Skipping because the request is for a physical file.");
                return;
            }

            var item = Context.Item;
            string itemUrl = LinkManager.GetItemUrl(item);
            string currentUrl = request.RawUrl;

            if (currentUrl.ToLower().StartsWith("/sitecore"))
            {
                Log.Debug("InvalidUrlProcessor: Skipping due to url containing /sitecore.");
                return;
            }

            if (currentUrl.ToLower().StartsWith("/?sc_itemid="))
            {
                Log.Debug("InvalidUrlProcessor: Skipping due to url containing /?sc_itemid=.");
                return;
            }

            // do not redirect for the 404 page
            if (Context.Site != null && Context.Database != null)
            {
                var pageNotFoundItem = Context.Database.GetItem(string.Concat(Context.Site.StartPath, Settings.ItemNotFoundUrl));
                if (pageNotFoundItem != null && Context.Item.ID == pageNotFoundItem.ID)
                {
                    return;
                }
            }
            
            // Remove the query string off the end if it has one.
            var query = request.Url.Query;
            var decodedQueryString = String.Empty;

            if (!string.IsNullOrEmpty(query))
            {
                decodedQueryString = args.Context.Server.UrlDecode(query);
                if (string.IsNullOrEmpty(decodedQueryString)) return;
                currentUrl = currentUrl.Replace(decodedQueryString, "");
                currentUrl = currentUrl.Replace(query, "");
            }

            Log.Debug("InvalidUrlProcessor: itemUrl=" + itemUrl);
            Log.Debug("InvalidUrlProcessor: currentUrl=" + currentUrl);

            if (itemUrl == currentUrl)
            {
                Log.Debug("InvalidUrlProcessor: URLs match so skipping this request.");
                return;
            }

            Log.Debug("InvalidUrlProcessor: URLs DONT match so 301 redirect to the proper version.");

            try
            {
                response.StatusCode = (int)HttpStatusCode.MovedPermanently;
                response.AddHeader("Location", itemUrl + decodedQueryString);
                response.End();
            }
            catch (ThreadAbortException)
            {
                // Ignore Thread abort as this is a natural exception caused by response.end 
                // which fills up our logs etc with false errors.
                return;
            }
        }
        protected virtual bool IsValidContextItemResolved()
        {
            if (Context.Item == null)
                return false;

            return !(Context.Item.Visualization.Layout == null
                     && string.IsNullOrEmpty(WebUtil.GetQueryString("sc_layout")));
        }

        protected virtual bool RequestIsForPhysicalFile(string filePath)
        {
            return File.Exists(HttpContext.Current.Server.MapPath(filePath));
        }
    }
}