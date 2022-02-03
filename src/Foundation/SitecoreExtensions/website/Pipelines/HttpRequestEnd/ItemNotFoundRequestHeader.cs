using Sitecore;
using Sitecore.Configuration;
using Sitecore.Pipelines.HttpRequest;
using System.IO;
using System.Web;

namespace LionTrust.Foundation.SitecoreExtensions.Pipelines.HttpRequestEnd
{
    public class ItemNotFoundRequestHeader : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (Context.Item == null
                || Context.Site == null
                || Context.Database == null
                || File.Exists(HttpContext.Current.Server.MapPath(args.Url.FilePath))
               )
            {
                return;
            }

            var pageNotFoundItem = Context.Database.GetItem(string.Concat(Context.Site.StartPath, Settings.ItemNotFoundUrl));
            if (pageNotFoundItem == null)
            {
                return;
            }

            if (Context.Item.ID == pageNotFoundItem.ID)
            {
                HttpContext.Current.Response.TrySkipIisCustomErrors = true;
                HttpContext.Current.Response.StatusCode = 404;
                HttpContext.Current.Response.StatusDescription = "Page not found";
            }            
        }
    }
}