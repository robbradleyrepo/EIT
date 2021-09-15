namespace LionTrust.Feature.Sitemap.Processors.DefaultProcessor
{
    using System.Collections.Generic;
    using System.Xml;
    using Sitecore.Configuration;
    using LionTrust.Feature.Sitemap.Pipelines;
    using Sitecore.Xml;

    public abstract class CreateSitemapXmlProcessor
    {
        private Dictionary<string, SiteDefinition> _configuration;

        protected Dictionary<string, SiteDefinition> Configuration
        {
            get
            {
                if (this._configuration == null)
                {
                    this._configuration = new Dictionary<string, SiteDefinition>();
                    foreach (XmlNode configNode in Factory.GetConfigNodes("sitemap/site"))
                    {
                        SiteDefinition siteDefinition = new SiteDefinition()
                        {
                            IncludedBaseTemplates = new List<string>(),
                            IncludedTemplates = new List<string>(),
                            ExcludedItems = new List<string>(),
                            EmbedLanguage = true,
                            SiteName = XmlUtil.GetAttribute("name", configNode),
                            IndexName = XmlUtil.GetAttribute("indexName", configNode)
                        };
                        string attribute = XmlUtil.GetAttribute("embedLanguage", configNode);
                        if (!string.IsNullOrEmpty(attribute))
                        {
                            bool result;
                            if (!bool.TryParse(attribute, out result))
                                result = true;
                            siteDefinition.EmbedLanguage = result;
                        }
                        XmlNode childNode1 = XmlUtil.FindChildNode("includeBaseTemplates", configNode, false);
                        XmlNode childNode2 = XmlUtil.FindChildNode("includeTemplates", configNode, false);
                        XmlNode childNode3 = XmlUtil.FindChildNode("excludeItems", configNode, false);
                        if (childNode1 != null)
                        {
                            foreach (XmlNode childNode4 in childNode1.ChildNodes)
                                siteDefinition.IncludedBaseTemplates.Add(childNode4.InnerText);
                        }
                        if (childNode2 != null)
                        {
                            foreach (XmlNode childNode4 in childNode2.ChildNodes)
                                siteDefinition.IncludedTemplates.Add(childNode4.InnerText);
                        }
                        if (childNode3 != null)
                        {
                            foreach (XmlNode childNode4 in childNode3.ChildNodes)
                                siteDefinition.ExcludedItems.Add(childNode4.InnerText);
                        }
                        this._configuration.Add(XmlUtil.GetAttribute("name", configNode), siteDefinition);
                    }
                }
                return this._configuration;
            }
        }

        public abstract void Process(CreateSitemapXmlArgs args);
    }
}