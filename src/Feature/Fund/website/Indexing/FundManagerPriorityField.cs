namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Priority = Foundation.Indexing.Priority;

    [Service(ServiceType = typeof(IPriority), Lifetime = Lifetime.Singleton)]
    public class FundManagerPriorityField : IPriority
    {
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(Constants.FundManager.TemplateId);
        }

        public int GetPriority()
        {
            return (int)Priority.FundManager;
        }
    }
}