namespace LionTrust.Foundation.Indexing.SiteSearch
{
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;

    public interface ISearchUrlField
    {
        bool CanHandle(IEnumerable<Guid> templateIds);

        string GetUrl(Item item);
    }
}