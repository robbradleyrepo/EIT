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
                Context.Item != null
                || Context.Site == null
                || Context.Database == null
                || File.Exists(HttpContext.Current.Server.MapPath(args.Url.FilePath))
               )
            {
                return;
            }

            // avoid api calls to return 404
            if (args.Url.FilePath.Contains("/sitecore"))
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