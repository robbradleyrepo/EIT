namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(ISearchResultType), Lifetime = Lifetime.Singleton)]
    public class DocumentSearchResultTypeField : ISearchResultType
    {
        private readonly Guid templateId = Guid.Parse(Foundation.Legacy.Constants.Document.TemplateId);
        
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(templateId);
        }

        public string GetSearchResultType()
        {
            return Sitecore.Globalization.Translate.Text("Document");
        }
    }
}