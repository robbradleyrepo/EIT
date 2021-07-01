namespace LionTrust.Feature.Fund.Indexing
{
    using LionTrust.Foundation.DI;
    using LionTrust.Foundation.Indexing.SiteSearch;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Service(ServiceType = typeof(ISearchResultType), Lifetime = Lifetime.Singleton)]
    public class FundDetailSearchResultTypeField : ISearchResultType
    {
        public bool CanHandle(IEnumerable<Guid> templateIds)
        {
            return templateIds.Contains(Constants.FundPage.TemplateId);
        }

        public string GetSearchResultType()
        {
            return Sitecore.Globalization.Translate.Text("Fund");
        }
    }
}