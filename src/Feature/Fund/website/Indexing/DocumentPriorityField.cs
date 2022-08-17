namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Priority = Foundation.Indexing.Priority;

    [Service(ServiceType = typeof(IPriority), Lifetime = Lifetime.Singleton)]
    public class DocumentPriorityField : IPriority
    {
        private readonly Guid templateId = Guid.Parse(Foundation.Legacy.Constants.Document.TemplateId);
        
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(templateId);
        }

        public int GetPriority()
        {
            return (int)Priority.Document;
        }
    }
}