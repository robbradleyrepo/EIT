namespace LionTrust.Feature.Sitemap.Pipelines
{
    using System.Collections.Generic;
    using Sitecore.Pipelines;
    using Sitecore.Web;

    public class CreateSitemapXmlArgs : PipelineArgs
    {
        public SiteInfo Site { get; private set; }

        public List<UrlDefinition> Nodes { get; private set; }

        public CreateSitemapXmlArgs(SiteInfo site)
        {
            this.Nodes = new List<UrlDefinition>();
            this.Site = site;
        }
    }
}
