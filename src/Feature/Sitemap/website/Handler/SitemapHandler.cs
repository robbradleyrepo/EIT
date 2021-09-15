namespace LionTrust.Feature.Sitemap.Handler
{
    using LionTrust.Feature.Sitemap.Pipelines;
    using Sitecore;
    using Sitecore.Caching;
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;
    using Sitecore.Web;
    using Sitecore.Xml;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Xml;

    public sealed class SitemapHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (DoProcessRequest(context))
            {
                return;
            }
            context.Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
        }

        private string BuildSitemap(SiteInfo site)
        {
            Context.SetActiveSite(site.Name);
            var stringBuilder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?><urlset xmlns:xhtml=\"http://www.w3.org/1999/xhtml\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

            var createSitemapXmlArgs = new CreateSitemapXmlArgs(site);
            CorePipeline.Run("createSitemapXml", createSitemapXmlArgs);

            foreach (var node in createSitemapXmlArgs.Nodes)
            {
                stringBuilder.Append("<url>");
                stringBuilder.AppendFormat($"<loc>{node.Location}</loc>");
                stringBuilder.AppendFormat($"<lastmod>{node.LastModified.ToString("yyyy-MM-dd")}</lastmod>");
                stringBuilder.Append("</url>");
            }
            stringBuilder.Append("</urlset>");
            return stringBuilder.ToString();
        }

        private bool DoProcessRequest(HttpContext context)
        {
            var siteInfo = Factory.GetSiteInfoList().FirstOrDefault(i => i.HostName.Contains(context.Request.Url.Host) &&
                                                                                   context.Request.Url.PathAndQuery.StartsWith(i.VirtualFolder, System.StringComparison.InvariantCultureIgnoreCase));

            if (siteInfo == null || siteInfo.Port > 0 && siteInfo.Port != context.Request.Url.Port)
            {
                Log.Error("Could not locate a sitemap entry.", this);
                return false;
            }
            else
            {
                var source = Factory.GetConfigNodes("sitemap/site").Cast<XmlNode>().Select(node => XmlUtil.GetAttribute("name", node));
                if (!source.Contains<string>(siteInfo.Name))
                {
                    Log.Error("Could not locate a sitemap entry using site name", this);
                    return false;
                }
            }

            var siteContext = Factory.GetSite(siteInfo.Name);
            var htmlCache = CacheManager.GetHtmlCache(siteContext);
            var cacheKey = $"{siteContext.Name}__sitemap";
            var xml = htmlCache?.GetHtml(cacheKey) ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(xml))
            {
                context.Response.ContentType = "text/xml";
                context.Response.Write(xml);
                return true;
            }

            context.Response.ContentType = "text/xml";
            xml = BuildSitemap(siteInfo);
            htmlCache?.SetHtml(cacheKey, xml);
            context.Response.Write(xml);
            return true;
        }
    }
}
