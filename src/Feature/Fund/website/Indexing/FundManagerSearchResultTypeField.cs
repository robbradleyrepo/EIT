namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(ISearchResultType), Lifetime = Lifetime.Singleton)]
    public class FundManagerSearchResultTypeField : ISearchResultType
    {
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(Constants.FundManager.TemplateId);
        }

        public string GetSearchResultType()
        {
            return Sitecore.Globalization.Translate.Text("Fund Manager");
        }
    }
}